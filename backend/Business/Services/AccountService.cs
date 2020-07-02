using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Dto.UserDto;
using backend.Business.Interfaces;
using backend.Controllers;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;

namespace backend.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; // All about the user
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager; // Allow to edit role
        private readonly IMapper _mapper;

        public AccountService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountService>();
            _configuration = configuration;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // registerAsync
        public async Task<UserInformationDto> RegisterAsync(UserInformationDto model)
        {
            var roleExistResult = _roleManager.RoleExistsAsync(model.Role); // check if the role exist
            if (!roleExistResult.Result)
                throw new CustomException($"The Role {model.Role} not found", HttpStatusCode.NotFound);

            ValidateMatchPasswords(model.Password, model.ConfirmPassword);

            ValidateMinPasswordLength(model.Password);

            var user = new ApplicationUser // Create the user that we want if the role exist
            {
                // Change from Dto to regular by mapping
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                SubRoleId = model.SubRole.Id,
                Image = model.Image
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleResult =
                    _userManager.AddToRoleAsync(user,
                        model.Role); // If the result succeeded we add the role to the user 

                if (roleResult.Result.Succeeded)
                {
                    return model;
                }

                _logger.LogError("Failed to add user " + user.UserName + " into role " + model.Role);
            }

            throw new CustomException(string.Join(',', result.Errors.Select(e => e.Description)));
            _logger.LogError("Failed to create user " + user.UserName);
            return null;
        }

        private static void ValidateMatchPasswords(string password, string confirmPassword)
        {
            if (!password.Trim().Equals(confirmPassword.Trim()))
                throw new CustomException("Passwords are not match");
        }

        private static void ValidateMinPasswordLength(string password)
        {
            if (password.Trim().Length < Constants.MIN_PASSWORD_LENGTH)
                throw new
                    CustomException(
                        $"The Password {password.Trim()} must be at least" +
                        $" {Constants.MIN_PASSWORD_LENGTH} and at max " +
                        $"{Constants.MAX_PASSWORD_LENGTH} characters long");
        }


        //LoginAsync
        public async Task<dynamic> LoginAsync(LoginDto model)
        {
            // signInManager if from the identity library
            var result =
                await _signInManager.PasswordSignInAsync(model.Username, model.Password, false,
                    false); // send the email and the password and confirm the password - check if the user connect

            if (!result.Succeeded) throw new CustomException($"User or Password are incorrect");
            var user = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count <= 0)
                throw new CustomException("User not assigned to any roles there for he cannot be logged in");

            var token = GenerateJwtToken(user, roles); // Creating user token
            return new {token, roles};
        }


        //change password - the user already connect
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto model, ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
                throw new CustomException(
                    $"Unable to load user with ID '{_userManager.GetUserId(userPrincipal)}'.");

            ValidateMatchPasswords(model.NewPassword, model.ConfirmNewPassword);

            ValidateMinPasswordLength(model.NewPassword);

            var result =
                await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                // var errors = result.Errors.Select(x => x.Description);
                throw new CustomException($"Change Password failed");
            }

            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordModel)
        {
            var user = _userManager.FindByNameAsync(resetPasswordModel.UserName).Result;
            if (user == null)
                throw new CustomException(
                    $"Unable to load user with username '{resetPasswordModel.UserName}'.");
            ValidateMatchPasswords(resetPasswordModel.Password, resetPasswordModel.ConfirmPassword);
            ValidateMinPasswordLength(resetPasswordModel.Password);

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!result.Succeeded)
            {
                throw new CustomException($"Reset Password failed");
            }

            return result;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordModel)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);

            // If the user is found AND Email is confirmed
            if (user == null) throw new CustomException("Not Success");

            // Generate the reset password token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            Console.WriteLine(token);
            var subject = "Forgot Password";
            var to = new EmailAddress(forgotPasswordModel.Email, user.NormalizedUserName);
            var bodyText = "Dear user, bla bla bla";
            var bodyHtml = $"<a href='http://localhost:4200/sessions/resetPassword?token={HttpUtility.UrlEncode(token)}'>" +
                           $"Dear user {user.FirstName} {user.LastName}," +
                           " In order to reset your password click here"+
                           "</a>";
            
            var sendEmailResult = await EmailService.SendEmail(to, subject, bodyText, bodyHtml);
            Console.WriteLine(sendEmailResult.StatusCode);
            if(sendEmailResult.StatusCode != HttpStatusCode.Accepted)
                throw new CustomException("Send email process failed.");
            return true;
        }

        public async Task<IdentityResult> UpdateCurrentUserAsync(UserInformationDto model,
            ClaimsPrincipal userPrincipal)
        {
            // Get the existing user from the db
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
                throw new CustomException($"Unable to load user with ID'.");

            // Update it with the values from the view model
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, model.Role);

            // Apply the changes if any to the db
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new CustomException($"Unable to update.");

            return result;
        }

        public async Task<UserInformationDto> GetCurrentUserAsync(ClaimsPrincipal userPrincipal)
        {
            var id = userPrincipal.FindFirst("Sub")?.Value;
            var result = await _userManager.Users
                .Include(x=> x.SubRole)
                .SingleOrDefaultAsync(x => x.Id == id);
            Console.WriteLine(result.SubRole);
            Console.WriteLine(result);
            // var role = await _userManager.GetRolesAsync(result);
            // result.Role = role;
            if (result == null)
                throw new CustomException($"User not found", HttpStatusCode.NotFound);
            return _mapper.Map<UserInformationDto>(result);
        }

        public async Task DeleteCurrentAccountAsync(ClaimsPrincipal userPrincipal)
        {
            var result = await _userManager.GetUserAsync(userPrincipal);
            if (result == null)
                throw new CustomException($"User not found", HttpStatusCode.NotFound);
            await _userManager.DeleteAsync(result);
            await _context.SaveChangesAsync();
        }


        // Generate token 
        private object GenerateJwtToken(ApplicationUser user, IList<string> userRole)
        {
            // remove unnecessary claims 
            // Information that we put on the basic token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                //new Claim("lastName", user.LastName),
                //new Claim("firstName", user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("email", user.Email)
            };

            claims.AddRange(userRole.Select(role => new Claim("roles", role))); // required

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Adding security
            var expires =
                DateTime.Now.AddDays(
                    Convert.ToDouble(
                        _configuration[
                            "JwtExpireDays"])); //"JwtExpireDays" days without login - Automatically disconnect the user 

            var token = new JwtSecurityToken( // create new token
                _configuration["JwtIssuer"], // token issuer name
                _configuration["JwtIssuer"],
                claims,
                expires: expires, // when this expire
                signingCredentials: credentials // things that required to signin (encoded)
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // Creating the token
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("admin").Result)
                _roleManager.CreateAsync(new IdentityRole("admin")).Wait();
            if (!_roleManager.RoleExistsAsync("user").Result)
                _roleManager.CreateAsync(new IdentityRole("user")).Wait();
            if (!_roleManager.RoleExistsAsync("developer").Result)
                _roleManager.CreateAsync(new IdentityRole("developer")).Wait();
        }
    }
}
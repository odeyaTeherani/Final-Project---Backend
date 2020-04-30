﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Controllers;
using backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace backend.Business.Services
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<ApplicationUser> _userManager; // All about the user
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager; // Allow to edit role

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountService>();
            _configuration = configuration;
            _roleManager = roleManager;
        }

        // registerAsync
        public async Task<UserInformationDto> RegisterAsync(UserInformationDto model)
        {
            var roleExistResult = _roleManager.RoleExistsAsync(model.Role); // check if the role exist
            if (!roleExistResult.Result) throw new CustomException($"The Role {model.Role} not found", HttpStatusCode.NotFound);

            var user = new ApplicationUser // Create the user that we want if the role exist
            {
                // Change from Dto to regular by mapping
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleResult = _userManager.AddToRoleAsync(user, model.Role); // If the result succeeded we add the role to the user 

                if (roleResult.Result.Succeeded)
                {
                    return model;
                }
                _logger.LogError("Failed to add user " + user.UserName + " into role " + model.Role);
            }
            _logger.LogError("Failed to create user " + user.UserName);
            return null;
        }


        //LoginAsync
        public async Task<dynamic> LoginAsync(LoginDto model)
        {
            // signInManager if from the identity library
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false); // send the email and the password and confirm the password - check if the user connect

            if (!result.Succeeded) throw new CustomException($"User or Password are incorrect"); // HttpStatusCode?
            var user = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count <= 0)
                throw new CustomException("User not assigned to any roles there for he cannot be logged in");

            var token = GenerateJwtToken(user, roles); // Creating user token
            return new { token, roles };
        }


        //change password - the user already connect
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto model)
        {
            var user = await _userManager.GetUserAsync(ClaimsPrincipal.Current);
            if (user == null)
                throw new CustomException($"Unable to load user with ID '{_userManager.GetUserId(ClaimsPrincipal.Current)}'.");

            var result =
                await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                // var errors = result.Errors.Select(x => x.Description);
                throw new CustomException("Change Password failed");
            }
            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserInformationDto model)
        {
            // Get the existing student from the db
            var user = await _userManager.GetUserAsync(ClaimsPrincipal.Current);
            if (user == null)
                throw new CustomException($"Unable to load user with ID '{_userManager.GetUserId(ClaimsPrincipal.Current)}'.");

            // Update it with the values from the view model
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, model.Role);

            // Apply the changes if any to the db
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new CustomException($"Unable to update.");
            
            return result;
        }


        // Generate token 
        private object GenerateJwtToken(ApplicationUser user, IList<string> userRole)
        {
            // remove unnecessary claims 
            // Information that we put on the basic token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("email",user.Email)
            };

            claims.AddRange(userRole.Select(role => new Claim("roles", role))); // required

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken( // create new token
                _configuration["JwtIssuer"],  // token issuer name
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
            {
                _roleManager.CreateAsync(new IdentityRole("admin")).Wait();
            }            
            if (!_roleManager.RoleExistsAsync("user").Result)
            {
                _roleManager.CreateAsync(new IdentityRole("user")).Wait();
            }
        }
    }
}



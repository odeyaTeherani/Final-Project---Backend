using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    public class AccountController : ControllerBase
    {
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<object> Register([FromBody]RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _account.RegisterAsync(model);

                if (result == null)
                {
                    return BadRequest("Role doesn't exist or username/mail are taken");
                }
                return Ok(model);
            }

            _logger.LogError("Failed to create user, bad parameters were sent");

            return BadRequest();
        }

        // Login 
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    var user = _userManager.Users.SingleOrDefault(r => r.UserName == model.Email);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Count <= 0)
                        return BadRequest("User not assigned to any roles there for he cannot be logged in");


                    var token = GenerateJwtToken(model.Email, user, roles);
                    return Ok(token);
                }
            }
            Console.WriteLine("INVALID_LOGIN_ATTEMPT");
            return BadRequest();
        }

        // Generate token 
        private async Task<object> GenerateJwtToken(string email, TenantUser user, IList<string> userRole)
        {
            // remove unnesseccery claims 
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim("name", user.UserName),
        new Claim("email",user.Email)
    };

            claims.AddRange(userRole.Select(role => new Claim("roles", role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                    _configuration["JwtIssuer"],
                    _configuration["JwtIssuer"],
                    claims,
                    expires: expires,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
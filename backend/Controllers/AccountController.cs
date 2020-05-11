using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            IAccountService account,
            UserManager<ApplicationUser> userManager)
        {
            _account = account;
            _userManager = userManager;
        }


        // Register
        [HttpPost("Register")]
        [AllowAnonymous] // Negates the Authorize Attribute and allows anonymous access
        public async Task<object> Register([FromBody] UserInformationDto model)
        {
            if (!ModelState.IsValid) // do validation on Dto's 
                return BadRequest();
            return Ok(await _account.RegisterAsync(model));
        }

        // Login 
        [HttpPost("Login")]
        [AllowAnonymous] // Negates the Authorize Attribute and allows anonymous access
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _account.LoginAsync(model));
        }


        //update user - TODO see in internet -  https://stackoverflow.com/questions/39545037/updating-user-by-usermanager-update-in-asp-net-identity-2
        [HttpPut("updateUser")]
        private async Task<IActionResult> UpdateUserAsync([FromBody] UserInformationDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _account.UpdateUserAsync(model));
        }


        //change password - the user already connect
        [HttpPut("changePassword")]
        private async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _account.ChangePasswordAsync(model));
        }


        //reset password - forget Password - TODO - nice to have
        /*[HttpPut("resetPassword")]
        [AllowAnonymous]
        private async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resertPasswordModel)
        {
            var user = _userManager.FindByNameAsync(resertPasswordModel.UserName).Result;
            var result =
                await _userManager.ResetPasswordAsync(user, resertPasswordModel.Token, resertPasswordModel.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }*/

    }

}
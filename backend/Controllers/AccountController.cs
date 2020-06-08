using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Dto.UserDto;
using backend.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;

        public AccountController(IAccountService account)
        {
            _account = account;
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


        //update user
        [Authorize]
        [HttpPut("updateCurrentUser")]
        public async Task<IActionResult> UpdateCurrentUserAsync([FromBody] UserInformationDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _account.UpdateCurrentUserAsync(model,User));
        }
        
        [Authorize]
        [HttpGet ("getCurrentUser")]
        public async Task<OkObjectResult> GetCurrentUser()
        {
            return Ok(await _account.GetCurrentUserAsync(User));
        }
        
        [Authorize]
        [HttpDelete("deleteCurrentAccount")]
        public IActionResult DeleteCurrentAccount()
        {
            _account.DeleteCurrentAccountAsync(User);
            return Ok();
        }


        //change password - the user already connect
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto model)
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
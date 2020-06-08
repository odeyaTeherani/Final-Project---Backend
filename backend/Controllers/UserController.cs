using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Business.Dto.UserDto;
using backend.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<List<UserInformationDto>> Get()
        {
            return await _userService.GetAllAsync();
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(UserInformationDto model, string id)
        {
            if (model == null) throw new CustomException($"The report is empty");
            await _userService.UpdateUserAsync(model, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<OkResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }

    }
}
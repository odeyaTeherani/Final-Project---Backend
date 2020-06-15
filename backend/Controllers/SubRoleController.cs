
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class SubRoleController: ControllerBase
    {
        private readonly ISubRoleService _subRoleService;
        
        public SubRoleController(ISubRoleService subRoleService)
        {
            _subRoleService = subRoleService;
        }
        
        [HttpGet]
        public async Task<List<SubRoleDto>> GetAsync()
        {
            return await _subRoleService.GetAllAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _subRoleService.GetByIdAsync(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddNewSubRoleAsync(string newSubRole)
        {
            if (newSubRole == null) throw new CustomException($"The new sub role is empty");
            var result = await _subRoleService.AddNewSubRoleAsync(newSubRole);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubRoleAsync(int id, [FromBody] SubRoleDto updateSubRole)
        {
            if (updateSubRole == null) throw new CustomException($"sub role is empty");
            var result = await _subRoleService.UpdateSubRoleAsync(id, updateSubRole);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            await _subRoleService.DeleteAsync(id);
            return Ok();
        }

    }
}
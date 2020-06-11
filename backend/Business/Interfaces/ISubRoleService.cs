using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface ISubRoleService
    {
        Task<List<SubRoleDto>> GetAllAsync();
        Task<SubRoleDto> GetByIdAsync(int id);
        Task<SubRoleDto> AddNewSubRoleAsync([FromBody] SubRoleDto newEventType);
        Task<SubRoleDto> UpdateSubRoleAsync(int id, [FromBody] SubRoleDto updateEventType);
        Task DeleteAsync(int id);
    }
}
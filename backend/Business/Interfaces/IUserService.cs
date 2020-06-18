using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Business.Dto.UserDto;

namespace backend.Business.Interfaces
{
    public interface IUserService
    {
        Task<List<UserInformationDto>> GetAllAsync(string name = null, string email = null,int? subRoleId = null);
        Task<UserInformationDto> GetByIdAsync(string id);
        Task UpdateUserAsync(UserInformationDto model, string id);
        
        Task DeleteAsync(string id);


    }
}
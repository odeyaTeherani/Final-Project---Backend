using System.Security.Claims;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Dto.UserDto;
using Microsoft.AspNetCore.Identity;

namespace backend.Business.Interfaces
{
    public interface IAccountService
    {
        Task<UserInformationDto> RegisterAsync(UserInformationDto model);
        Task<dynamic> LoginAsync(LoginDto model);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto model);
        Task<IdentityResult> UpdateCurrentUserAsync(UserInformationDto model, ClaimsPrincipal user);
        Task<UserInformationDto> GetCurrentUserAsync(ClaimsPrincipal userPrincipal);
        Task DeleteCurrentAccountAsync(ClaimsPrincipal userPrincipal);
        void SeedRoles();
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using backend.Business.Dto.UserDto;
using backend.Business.Helpers;
using backend.Business.Interfaces;
using backend.Controllers;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Business.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context, IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<UserInformationDto>> GetAllAsync(string name = null, string email = null, int? subRoleId = null)
        {
            if (ShouldFilter(name, email, subRoleId))
            {
                var all = await _userManager.Users.
                    FilterUsers(name,email,subRoleId).ToListAsync();
                var orderByDescending = all.OrderByDescending(e => e.LastName);
                return _mapper.Map<List<UserInformationDto>>(orderByDescending);
            }
            else
            {
                var all = await _userManager.Users.ToListAsync();
                var orderByDescending = all.OrderByDescending(e => e.LastName);
                return _mapper.Map<List<UserInformationDto>>(orderByDescending);
            }
        }

        public async Task<UserInformationDto> GetByIdAsync(string id)
        {
            var result = await _userManager
                .Users
                .Include(x => x.SubRole)
                .SingleOrDefaultAsync(x => x.Id == id);
            
            if (result == null)
                throw new CustomException($"User with id {id} not found", HttpStatusCode.NotFound);
            return _mapper.Map<UserInformationDto>(result);
        }

        public async Task UpdateUserAsync(UserInformationDto model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new CustomException($"User with id {id} not found", HttpStatusCode.NotFound);
            // Update it with the values from the view model
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Image = model.Image;

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, model.Role);

            // Apply the changes if any to the db
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new CustomException($"Unable to update.");
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new CustomException($"User with id {id} not found", HttpStatusCode.NotFound);
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        private bool ShouldFilter(string name = null, string email = null, int? subRoleId = null)
        {
            return subRoleId.HasValue || name != null || email != null;
        }
    }
}
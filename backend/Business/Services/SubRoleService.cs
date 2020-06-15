using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Data;
using backend.Data.Models;
using backend.Controllers;
using Microsoft.EntityFrameworkCore;

namespace backend.Business.Services
{
    public class SubRoleService: ISubRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SubRoleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<SubRoleDto>> GetAllAsync()
        {
            var all = await _context.SubRoles.ToListAsync();
            return _mapper.Map<List<SubRoleDto>>(all);
        }

        public async Task<SubRoleDto> GetByIdAsync(int id)
        {
            var result = await _context.SubRoles.
                SingleOrDefaultAsync(e => e.Id == id);
            if (result == null) 
                throw new CustomException($"Sub Role whit id {id} not found", HttpStatusCode.NotFound);
            return _mapper.Map<SubRoleDto>(result);
        }

        public async Task<SubRoleDto> AddNewSubRoleAsync(string newSubRole)
        {
            var newSub = new SubRole {Name = newSubRole.Trim()};
            await _context.SubRoles.AddAsync(newSub);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubRoleDto>(newSub);
        }

        public async Task<SubRoleDto> UpdateSubRoleAsync(int id, SubRoleDto updateSubRole)
        {
            var result = await _context.SubRoles.
                SingleOrDefaultAsync(e => e.Id == id);
            if (result == null) 
                throw new CustomException($"Sub Role whit id {id} not found", HttpStatusCode.NotFound);
            
            result.Name = updateSubRole.Name;

            await _context.SaveChangesAsync();
            return _mapper.Map<SubRoleDto>(result);
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.SubRoles.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id); // Make sure it is single and if you didnt find return null
            if (result == null) 
                throw new CustomException($"Sub Role whit id {id} not found", HttpStatusCode.NotFound);
            _context.SubRoles.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
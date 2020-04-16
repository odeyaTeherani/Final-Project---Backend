using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Data;

namespace backend.Business.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<UserDto> GetAll()
        {
            var all = _context.Users.ToList();
            return _mapper.Map<List<UserDto>>(all);
        }

        public UserDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto AddNewUser(UserDto newUser)
        {
            throw new NotImplementedException();
        }

        public UserDto UpdateUser(int id, UserDto updateUser)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

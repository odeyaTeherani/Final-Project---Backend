using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    interface IUserService
    {
        List<UserDto> GetAll();
        UserDto GetById(int id);
        UserDto AddNewUser([FromBody] UserDto newUser);
        UserDto UpdateUser(int id, [FromBody] UserDto updateUser);
        void Delete(int id);
    }
}

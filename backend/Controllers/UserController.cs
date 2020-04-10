using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        // https://localhost:44341/user
        [HttpGet]
        public List<User> Get()
        {
            return ApplicationDbContext.Users;
        }

        // https://localhost:44341/user/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = ApplicationDbContext.Users.Find(e => e.Id == id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // https://localhost:44341/user
        [HttpPost]
        public IActionResult AddNewUser([FromBody] User newUser)
        {
            if (newUser == null) return BadRequest();
            newUser.Id = ApplicationDbContext.Users.Max(e => e.Id) + 1;
            ApplicationDbContext.Users.Add(newUser);
            return CreatedAtAction("GetById", new {id = newUser.Id}, newUser);

        }

        // Don't work
        // One of the parameters are empty
        // https://localhost:44341/user/{id}
        [HttpPut("id")]
        public IActionResult Put(int id, [FromBody] User updateUser)
        {
            if (updateUser == null) return BadRequest();

            var result = ApplicationDbContext.Users.Find(e => e.Id == id);
            if (result == null) return NotFound();

            result.userName = updateUser.userName;
            result.Password = updateUser.Password;

            return NoContent();
        }

        // https://localhost:44341/user/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = ApplicationDbContext.Users.Find(e => e.Id == id);
            if (result == null) return NotFound();

            ApplicationDbContext.Users.Remove(result);
            return Ok();
        }
    }
}

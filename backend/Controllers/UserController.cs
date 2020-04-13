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
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // https://localhost:44341/user
        [HttpGet]
        public List<User> Get()
        {
            return _context.Users.ToList();
        }

        // https://localhost:44341/user/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _context.Users.SingleOrDefault(e => e.Id == id);  // Make sure it is single and if you didn't find return null
            if (result == null) return NotFound();
            return Ok(result);
        }

        // https://localhost:44341/user
        [HttpPost]
        public IActionResult AddNewUser([FromBody] User newUser)
        {
            if (newUser == null) return BadRequest();
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return CreatedAtAction("GetById", new {id = newUser.Id}, newUser);
        }

        // One of the parameters are empty
        // https://localhost:44341/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updateUser)
        {
            if (updateUser == null) return BadRequest();

            var result = _context.Users.SingleOrDefault(e => e.Id == id);
            if (result == null) return NotFound();

            result.UserName = updateUser.UserName;
            result.Password = updateUser.Password;

            _context.SaveChanges();
            return NoContent();
        }

        // https://localhost:44341/user/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _context.Users.SingleOrDefault(e => e.Id == id);
            if (result == null) return NotFound();

            _context.Users.Remove(result);
            _context.SaveChanges();
            return Ok();
        }
    }
}

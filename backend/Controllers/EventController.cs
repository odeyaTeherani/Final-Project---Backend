using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // https://localhost:44341/event
        [HttpGet]
        public List<Event> Get()
        {
            throw new Exception("abc");
            return _context.Events.ToList();
        }

        // https://localhost:44341/event/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _context.Events.SingleOrDefault(e => e.Id == id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // https://localhost:44341/event
        [HttpPost]
        public IActionResult AddNewEvent([FromBody] Event newEvent)
        {
            if (newEvent == null) return BadRequest();
            _context.Events.Add(newEvent);
            _context.SaveChanges();
            return CreatedAtAction("GetById", new {id = newEvent.Id}, newEvent);

        }

        // One of the parameters are empty
        // https://localhost:44341/event/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event updateEvent)
        {
            var result = _context.Events.SingleOrDefault(e => e.Id == id);
            if (result == null) return NotFound();

            result.EventType = updateEvent.EventType;
            result.Location = updateEvent.Location;
            result.SeverityLevelType = updateEvent.SeverityLevelType;
            result.Date = updateEvent.Date;
            _context.SaveChanges();
            return NoContent();
        }

        // https://localhost:44341/event/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _context.Events.SingleOrDefault(e => e.Id == id);
            if (result == null) return NotFound();

            _context.Events.Remove(result);
            _context.SaveChanges();
            return Ok();
        }
    }
}
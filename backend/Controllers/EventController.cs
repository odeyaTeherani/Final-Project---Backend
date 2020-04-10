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
    public class EventController : ControllerBase
    {
        // https://localhost:44341/event
        [HttpGet]
        public List<Event> Get()
        {
            return ApplicationDbContext.Events;
        }

        // https://localhost:44341/event/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = ApplicationDbContext.Events.Find(e => e.Id == id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // https://localhost:44341/event
        [HttpPost]
        public IActionResult AddNewEvent([FromBody] Event newEvent)
        {
            if (newEvent == null) return BadRequest();
            newEvent.Id = ApplicationDbContext.Events.Max(e => e.Id) + 1;
            ApplicationDbContext.Events.Add(newEvent);
            return CreatedAtAction("GetById", new {id = newEvent.Id}, newEvent);

        }

        // Don't work
        // One of the parameters are empty
        // https://localhost:44341/event/{id}
        [HttpPut("{id}")]
        public IActionResult updateEvent(int id, [FromBody] Event updateEvent)
        {
            var result = ApplicationDbContext.Events.Find(e => e.Id == id);
            if (result == null) return NotFound();

            result.EventType = updateEvent.EventType;
            result.Location = updateEvent.Location;
            result.SeverityLevel = updateEvent.SeverityLevel;
            result.Date = updateEvent.Date;
            
            return NoContent();
        }

        // https://localhost:44341/event/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = ApplicationDbContext.Events.Find(e => e.Id == id);
            if (result == null) return NotFound();

            ApplicationDbContext.Events.Remove(result);
            return Ok();
        }
    }
}

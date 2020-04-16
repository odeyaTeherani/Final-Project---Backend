using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Interfaces;
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
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService; ;
        }

        // https://localhost:44341/event
        [HttpGet]
        public List<EventDto> Get()
        {
            return _eventService.GetAll();
        }

        // https://localhost:44341/event/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _eventService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // https://localhost:44341/event
        [HttpPost]
        public IActionResult AddNewEvent([FromBody] EventDto newEvent)
        {
            if (newEvent == null) return BadRequest();
            var result = _eventService.AddNewEvent(newEvent);
            return CreatedAtAction("GetById", new {id = newEvent.Id}, newEvent);

        }

        // One of the parameters are empty
        // https://localhost:44341/event/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] EventDto updateEvent)
        {
            if (updateEvent == null) return BadRequest();

            var result = _eventService.UpdateEvent(id, updateEvent);
            if (result == null) return NotFound();
            return NoContent();
        }

        // https://localhost:44341/event/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _eventService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<List<EventDto>> GetAsync()
        {
            return await _eventService.GetAllAsync();
        }

        // https://localhost:44341/event/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _eventService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // https://localhost:44341/event
        [HttpPost]
        public async Task<IActionResult> AddNewEventAsync([FromBody] EventDto newEvent)
        {
            if (newEvent == null) return BadRequest();
            var result = await _eventService.AddNewEventAsync(newEvent);
            return CreatedAtAction("GetByIdAsync", new {id = newEvent.Id}, newEvent);

        }

        // One of the parameters are empty
        // https://localhost:44341/event/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventAsync(int id, [FromBody] EventDto updateEvent)
        {
            if (updateEvent == null) return BadRequest();

            var result = await _eventService.UpdateEventAsync(id, updateEvent);
            if (result == null) return NotFound();
            return NoContent();
        }

        // https://localhost:44341/event/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAsync(int id)
        {
            try
            {
                _eventService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
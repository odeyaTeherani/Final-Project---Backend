using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
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
            _eventService = eventService;
        }

        // https://localhost:44341/event
        [HttpGet]
        public async Task<List<EventDto>> GetAsync()
        {
            // consider to implement paging mechanism 
            return await _eventService.GetAllAsync();
        }

        // https://localhost:44341/event/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _eventService.GetByIdAsync(id);
            return Ok(result);
        }

        // https://localhost:44341/eventff
        [HttpPost]
        public async Task<IActionResult> AddNewEventAsync([FromBody] EventDto newEvent)
        {
            var username = User.FindFirst("Sub")?.Value;
            if (newEvent == null) throw new CustomException($"The new event is empty");
            var result = await _eventService.AddNewEventAsync(newEvent, username);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }

        // One of the parameters are empty
        // https://localhost:44341/event/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventAsync(int id, [FromBody] EventDto updateEvent)
        {
            if (updateEvent == null) throw new CustomException($"The event is empty");
            await _eventService.UpdateEventAsync(id, updateEvent);
            return NoContent();
        }

        // https://localhost:44341/event/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAsync(int id)
        {
             _eventService.DeleteAsync(id);
             return Ok();
        }
    }
}
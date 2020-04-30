using System.Collections.Generic;
using System.Net;
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
            _eventService = eventService;
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
            return Ok(result);
        }

        // https://localhost:44341/event
        [HttpPost]
        public async Task<IActionResult> AddNewEventAsync([FromBody] EventDto newEvent)
        {
            if (newEvent == null) throw new CustomException($"The new event is empty", HttpStatusCode.BadRequest);
            var result = await _eventService.AddNewEventAsync(newEvent);
            return CreatedAtAction("GetByIdAsync", new { id = newEvent.Id }, result);

        }

        // One of the parameters are empty
        // https://localhost:44341/event/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventAsync(int id, [FromBody] EventDto updateEvent)
        {
            if (updateEvent == null) throw new CustomException($"The event is empty", HttpStatusCode.BadRequest);
            var result = await _eventService.UpdateEventAsync(id, updateEvent);
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
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
    public class EventTypeController : ControllerBase
    {
        private readonly IEventTypeService _eventTypeService;

        public EventTypeController(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }


        // https://localhost:44341/eventType
        [HttpGet]
        public async Task<List<EventTypeDto>> GetAsync()
        {
            return await _eventTypeService.GetAllAsync();
        }


        // https://localhost:44341/eventType/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _eventTypeService.GetByIdAsync(id);
            return Ok(result);
        }


        // https://localhost:44341/eventType
        [HttpPost]
        public async Task<IActionResult> AddNewEventTypeAsync([FromBody] EventTypeDto newEventType)
        {
            if (newEventType == null) throw new CustomException($"The new event type is empty", HttpStatusCode.BadRequest);
            var result = await _eventTypeService.AddNewEventTypeAsync(newEventType);
            return CreatedAtAction("GetByIdAsync", new {id = result.Id}, result);
        }


        // One of the parameters are empty
        // https://localhost:44341/eventType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventTypeAsync(int id, [FromBody] EventTypeDto updateEventType)
        {
            if (updateEventType == null) throw new CustomException($"event type is empty", HttpStatusCode.BadRequest);
            var result = await _eventTypeService.UpdateEventTypeAsync(id, updateEventType);
            return NoContent();
        }


        // https://localhost:44341/eventType/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAsync(int id)
        {

            _eventTypeService.DeleteAsync(id);
            return Ok();
        }
    }
}

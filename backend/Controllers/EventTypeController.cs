using System;
using System.Collections.Generic;
using backend.Business.Dto;
using backend.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventTypeController: ControllerBase
    {
        private readonly IEventTypeService _eventTypeService;

        public EventTypeController(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }


        // https://localhost:44341/eventType
        [HttpGet]
        public List<EventTypeDto> Get()
        {
            return _eventTypeService.GetAll();
        }


        // https://localhost:44341/eventType/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _eventTypeService.GetById(id);
            //if (result == null) return NotFound();
            return Ok(result);
        }


        // https://localhost:44341/eventType
        [HttpPost]
        public IActionResult AddNewEventType([FromBody] EventTypeDto newEventType)
        {
            //if (newEventType == null) return BadRequest();
            var result = _eventTypeService.AddNewEventType(newEventType);
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }


        // One of the parameters are empty
        // https://localhost:44341/eventType/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEventType(int id, [FromBody] EventTypeDto updateEventType)
        {
            //if (updateEventType == null) return BadRequest();

            var result = _eventTypeService.UpdateEventType(id, updateEventType);
            if (result == null) return NotFound();
            return NoContent();
        }


        // https://localhost:44341/eventType/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _eventTypeService.Delete(id);
            return Ok();
        }
    }
}

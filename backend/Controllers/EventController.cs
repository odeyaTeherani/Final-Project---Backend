using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Data.Enums;
using backend.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // https://localhost:44341/event
        [HttpGet]
        public async Task<List<EventDto>> Get(DateTime? date = null,int? eventTypeId= null,SeverityLevel? severityLevel = null)
        {
            // consider to implement paging mechanism 
            return await _eventService.GetAllAsync(date,eventTypeId,severityLevel);
        }        
        
        [HttpGet("consult")]
        public async Task<ConsultDto> GetConsult(int? hours = null,int? eventTypeId= null,SeverityLevel? severityLevel = null)
        {
            return await _eventService.GetConsultAsync(hours,eventTypeId,severityLevel);
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
        public async Task<IActionResult> AddNewEvent([FromBody] EventDto newEvent)
        {
            var username = User.FindFirst("Sub")?.Value;
            if (newEvent == null) throw new CustomException($"The new event is empty");
            var result = await _eventService.AddNewEventAsync(newEvent, username);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }

        // One of the parameters are empty
        // https://localhost:44341/event/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto updateEvent)
        {
            if (updateEvent == null) throw new CustomException($"The event is empty");
            await _eventService.UpdateEventAsync(id, updateEvent);
            return NoContent();
        }

        // https://localhost:44341/event/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             await _eventService.DeleteAsync(id);
             return Ok();
        }
        
        /*[AllowAnonymous]
        [HttpGet("seedEvents")]
        public async Task<IActionResult> SeedEvents()
        {
            try
            {
                await _eventService.ABC();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }*/
        
    }
}
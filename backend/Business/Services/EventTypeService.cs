using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Controllers;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Business.Services
{
    public class EventTypeService : IEventTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EventTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EventTypeDto>> GetAllAsync()
        {
            var all = await _context.EventTypes.ToListAsync();
            return _mapper.Map<List<EventTypeDto>>(all);
        }

        public async Task<EventTypeDto> GetByIdAsync(int id)
        {
            var result = await _context.EventTypes.SingleOrDefaultAsync(e => e.Id == id);
            if (result == null) throw new CustomException($"Event Type whit id {id} not found", HttpStatusCode.NotFound);
            return _mapper.Map<EventTypeDto>(result);
        }

        public async Task<EventTypeDto> AddNewEventTypeAsync(EventTypeDto newEventType)
        {
            var mapperEventType = _mapper.Map<EventType>(newEventType);
            await _context.EventTypes.AddAsync(mapperEventType);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventTypeDto>(mapperEventType);
        }

        public async Task<EventTypeDto> UpdateEventTypeAsync(int id, EventTypeDto updateEventType)
        {
            var result = await _context.EventTypes.SingleOrDefaultAsync(e => e.Id == id);
            if (result == null) throw new CustomException($"Event Type whit id {id} not found", HttpStatusCode.NotFound);
            
            result.Type = updateEventType.Type;

            await _context.SaveChangesAsync();
            return _mapper.Map<EventTypeDto>(result);
        }

        public async void DeleteAsync(int id)
        {
            var result = await _context.EventTypes.SingleOrDefaultAsync(e => e.Id == id); // Make sure it is single and if you didnt find return null
            if (result == null) throw new CustomException($"Event Type whit id {id} not found", HttpStatusCode.NotFound);
            _context.EventTypes.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}

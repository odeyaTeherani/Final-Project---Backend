using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Helpers;
using backend.Business.Interfaces;
using backend.Controllers;
using backend.Data;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Business.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private QueryHelper<Event> _queryHelper;

        public EventService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _queryHelper = new QueryHelper<Event>(_context);
        }

        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns>List of all events</returns>
        public async Task<List<EventDto>> GetAllAsync()
        {
            var all = await GetEvent()
                .ToListAsync();
            return _mapper.Map<List<EventDto>>(all);
        }

        public async Task<EventDto> GetByIdAsync(int id)
        {
            var result = await GetEvent()
                .SingleOrDefaultAsync(e => e.Id == id);
            if (result == null) throw new CustomException($"Event whit id {id} not found", HttpStatusCode.NotFound);
            return _mapper.Map<EventDto>(result);
        }

        public async Task<EventDto> AddNewEventAsync(EventDto newEvent, string userName)
        {
            var mapperEvent = _mapper.Map<Event>(newEvent);
            await _context.Events.AddAsync(mapperEvent);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventDto>(mapperEvent);
        }

        public async Task UpdateEventAsync(int id, EventDto updateEvent)
        {
            var result = await _context.Events.SingleOrDefaultAsync(e => e.Id == id);
            if (result == null) throw new CustomException($"Event whit id {id} not found", HttpStatusCode.NotFound);

            result.Date = updateEvent.Date;
            result.EventType = updateEvent.EventType;
            result.Images = updateEvent.Images;
            result.Location = updateEvent.Location;
            result.Reports = updateEvent.Reports;
            result.Date = updateEvent.Date;
            result.SeverityLevelType = updateEvent.SeverityLevelType;
            await _context.SaveChangesAsync();
        
        }

        public async void DeleteAsync(int id)
        {
            var result = await _context.Events.SingleOrDefaultAsync(e => e.Id == id); // Make sure it is single and if you didn't find return null
            if (result == null) throw new CustomException($"Event whit id {id} not found", HttpStatusCode.NotFound);
            _context.Events.Remove(result);
            await _context.SaveChangesAsync();
        }
        
        private IQueryable<Event> GetEvent()
        {
            return _queryHelper.GetAllIncluding(x => x.EventType, x => x.Images, x => x.Location);
        }
    }
}

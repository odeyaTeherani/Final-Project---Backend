using System;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Business.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly QueryHelper<Event> _queryHelper;

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
             var orderByDescending = all.OrderByDescending(e => e.StartDate);
            return _mapper.Map<List<EventDto>>(orderByDescending);
        }

        public async Task<EventDto> GetByIdAsync(int id)
        {
            var result = await GetEvent()
                .SingleOrDefaultAsync(e => e.Id == id);
            if (result == null) throw new CustomException($"Event whit id {id} not found", HttpStatusCode.NotFound);
            return _mapper.Map<EventDto>(result);
        }

        public async Task<EventDto> AddNewEventAsync([FromBody] EventDto newEvent, string userName)
        {
            var mappedEvent = _mapper.Map<Event>(newEvent);
            var reportsIds = mappedEvent.Reports?.Select(report => report.Id).ToList();
            mappedEvent.Reports = null;
            mappedEvent.CreateDate = DateTime.Now;
            await _context.Events.AddAsync(mappedEvent);

            var originalReports = _context.Reports.Where(report => reportsIds.Contains(report.Id)).ToList();
            originalReports.ForEach(report => { report.Event = mappedEvent; });
            
            await _context.SaveChangesAsync();
            return _mapper.Map<EventDto>(mappedEvent);
        }

        public async Task UpdateEventAsync(int id, EventDto updateEvent)
        {
            var result = await _context.Events.SingleOrDefaultAsync(e => e.Id == id);
            if (result == null) throw new CustomException($"Event whit id {id} not found", HttpStatusCode.NotFound);
            
            var mappedEvent = _mapper.Map<Event>(updateEvent);
            result.StartDate = updateEvent.StartDate;
            result.EndDate = updateEvent.EndDate;
            result.CreateDate = mappedEvent.CreateDate;
            result.NameInCharge = mappedEvent.NameInCharge;
            result.NumOfZakaCars = mappedEvent.NumOfZakaCars;
            result.NumOfAmbulances = mappedEvent.NumOfAmbulances;
            result.NumOfDead = mappedEvent.NumOfDead;
            result.NumOfEnvironment = mappedEvent.NumOfEnvironment;
            result.NumOfFirefighters = mappedEvent.NumOfFirefighters;
            result.NumOfInjured = mappedEvent.NumOfInjured;
            result.NumOfPolice = mappedEvent.NumOfPolice;
            result.Note = mappedEvent.Note;
            result.EventTypeId = updateEvent.EventType.Id;
            // result.Images. = mappedEvent.Images.;
           // result.Location = updateEvent.Location;
            // result.Reports = updateEvent.Reports;
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

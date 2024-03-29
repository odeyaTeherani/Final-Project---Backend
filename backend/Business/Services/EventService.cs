﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Helpers;
using backend.Business.Interfaces;
using backend.Controllers;
using backend.Data;
using backend.Data.Enums;
using backend.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace backend.Business.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _environment;
        private readonly QueryHelper<Event> _queryHelper;

        public EventService(ApplicationDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory,IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _environment = environment;
            _queryHelper = new QueryHelper<Event>(_context);
        }

        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns>List of all events</returns>
        public async Task<List<EventDto>> GetAllAsync(DateTime? date = null, int? eventTypeId = null,
            SeverityLevel? severityLevel = null)
        {
            if (ShouldFilter(date, eventTypeId, severityLevel))
            {
                var all = await GetEvent()
                    .FilterEvents(date, eventTypeId, severityLevel)
                    .ToListAsync();

                var orderByDescending = all.OrderByDescending(e => e.StartDate);
                return _mapper.Map<List<EventDto>>(orderByDescending);
            }
            else
            {
                var all = await GetEvent()
                    .ToListAsync();
                var orderByDescending = all.OrderByDescending(e => e.StartDate);
                return _mapper.Map<List<EventDto>>(orderByDescending);
            }
        }

        public async Task<ConsultDto> GetConsultAsync(int? hours = null, int? eventTypeId = null,
            SeverityLevel? severityLevel = null)
        {
            var url = _environment.IsDevelopment() ? "http://127.0.0.1:7000?event_type=" : "https://unify-ml-app.herokuapp.com?event_type=";
            var request = new HttpRequestMessage(HttpMethod.Get,
                url + eventTypeId + "&severity_level=" + (int) severityLevel +
                "&date_diff=" + hours);

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<ConsultDto>();
                return data;
            }
            else
            {
                throw new CustomException("connect to server failed");
            }
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
            var result = await _context.Events.
                Include(x=>x.Images)
                .SingleOrDefaultAsync(e => e.Id == id);
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
            // result.Images.AddRange(mappedEvent.Images);
            // result.Images = mappedEvent.Images;
            result.Location = mappedEvent.Location;
            result.SeverityLevelType = updateEvent.SeverityLevelType;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = 
                await _context.Events.SingleOrDefaultAsync(e =>
                    e.Id == id); // Make sure it is single and if you didn't find return null
            if (result == null) throw new CustomException($"Event whit id {id} not found", HttpStatusCode.NotFound);
            _context.Events.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task ABC()
        {
            var envents = new List<Event>();
            var random = new Random(); 
            for (int i = 0; i < 10000; i++)
            {
                var startDate = DateTime.Now.AddDays(random.Next(1, 50));
                var endDate = startDate.AddDays(random.Next(1, 5));
                envents.Add(new Event
                {
                    CreateDate = endDate.AddDays(1),
                    StartDate = startDate,
                    EndDate = endDate,
                    NumOfAmbulances = random.Next(1, 6),
                    NumOfInjured = random.Next(1, 35),
                    NumOfEnvironment = random.Next(1, 4),
                    NumOfPolice = random.Next(1, 6),
                    NumOfDead = random.Next(1, 5),
                    NumOfFirefighters = random.Next(1, 7),
                    NumOfZakaCars = random.Next(1, 4),
                    Note = $"Test Not {i}",
                    EventTypeId = random.Next(1, 3),
                    SeverityLevelType = (SeverityLevel)random.Next(1, 5),
                    NameInCharge = "system"
                });
            }

            await _context.Events.AddRangeAsync(envents);
            await _context.SaveChangesAsync();
        }

        private IQueryable<Event> GetEvent()
        {
            return _queryHelper.GetAllIncluding(x => x.EventType, x => x.Images, x => x.Location);
        }
        
     

        private bool ShouldFilter(DateTime? date = null, int? eventTypeId = null, SeverityLevel? severityLevel = null)
        {
            return date.HasValue || eventTypeId.HasValue || severityLevel.HasValue;
        }
    }
}

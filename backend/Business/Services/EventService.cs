using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Data;
using backend.Data.Models;

namespace backend.Business.Services
{
    public class EventService : IEventService

    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EventService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public List<EventDto> GetAll()
        {
            var all = _context.Events.ToList();
            return _mapper.Map<List<EventDto>>(all);
        }

        public EventDto GetById(int id)
        {
            var result = _context.Events.SingleOrDefault(e => e.Id == id);
            if (result == null) return null;
            return _mapper.Map<EventDto>(result);
        }

        public EventDto AddNewEvent(EventDto newEvent)
        {
            var mapperEvent = _mapper.Map<Event>(newEvent);
            _context.Events.Add(mapperEvent);
            _context.SaveChanges();
            return _mapper.Map<EventDto>(mapperEvent);
        }

        public EventDto UpdateEvent(int id, EventDto updateEvent)
        {
            var result = _context.Events.SingleOrDefault(e => e.Id == id);
            if (result == null) return null;

            result.Date = updateEvent.Date;
            result.EventType = updateEvent.EventType;
            result.Images = updateEvent.Images;
            result.Location = updateEvent.Location;
            result.Reports = updateEvent.Reports;
            result.Date = updateEvent.Date;
            result.SeverityLevelType = updateEvent.SeverityLevelType;

            _context.SaveChanges();
            return _mapper.Map<EventDto>(result);
        }

        public void Delete(int id)
        {
            var result = _context.Events.SingleOrDefault(e => e.Id == id); // Make sure it is single and if you didnt find return null
            if (result == null) throw new Exception($"not found id {id}");
            _context.Events.Remove(result);
            _context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Mvc;

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

        public List<EventTypeDto> GetAll()
        {
            var all = _context.EventTypes.ToList();
            return _mapper.Map<List<EventTypeDto>>(all);
        }

        public EventTypeDto GetById(int id)
        {
            var result = _context.EventTypes.SingleOrDefault(e => e.Id == id);
            if (result == null) return null;
            return _mapper.Map<EventTypeDto>(result);
        }

        public EventTypeDto AddNewEventType([FromBody] EventTypeDto newEventType)
        {
            var mapperEventType = _mapper.Map<EventType>(newEventType);
            _context.EventTypes.Add(mapperEventType);
            _context.SaveChanges();
            return _mapper.Map<EventTypeDto>(mapperEventType);
        }

        public EventTypeDto UpdateReport(int id, [FromBody] EventTypeDto updateEventType)
        {
            var result = _context.EventTypes.SingleOrDefault(e => e.Id == id);
            if (result == null) return null;

            result.Type = updateEventType.Type;

            _context.SaveChanges();
            return _mapper.Map<EventTypeDto>(result);
        }

        public void Delete(int id)
        {
            var result = _context.EventTypes.SingleOrDefault(e => e.Id == id); // Make sure it is single and if you didnt find return null
            if (result == null) throw new Exception($"not found id {id}");
            _context.EventTypes.Remove(result);
            _context.SaveChanges();
        }

    }
}

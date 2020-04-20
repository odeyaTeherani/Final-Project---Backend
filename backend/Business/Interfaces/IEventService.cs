using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IEventService
    {
        List<EventDto> GetAll();
        EventDto GetById(int id);
        EventDto AddNewEvent([FromBody] EventDto newEvent);
        EventDto UpdateEvent(int id, [FromBody] EventDto updateEvent);
        void Delete(int id);
    }
}

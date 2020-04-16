using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IEventTypeService
    {
        List<EventTypeDto> GetAll();
        EventTypeDto GetById(int id);
        EventTypeDto AddNewEventType([FromBody] EventTypeDto newEventType);
        EventTypeDto UpdateEventType(int id, [FromBody] EventTypeDto updateEventType);
        void Delete(int id);
    }
}

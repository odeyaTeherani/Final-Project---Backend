using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Data.Enums;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync(DateTime? date = null,int? eventTypeId= null,SeverityLevel? severityLevel = null);
        Task<ConsultDto> GetConsultAsync(int? hours = null, int? eventTypeId = null,
            SeverityLevel? severityLevel = null);
        Task<EventDto> GetByIdAsync(int id);
        Task<EventDto> AddNewEventAsync([FromBody] EventDto newEvent, string userName);
        Task UpdateEventAsync(int id, EventDto updateEvent);
        Task DeleteAsync(int id);
        Task ABC();
    }
}

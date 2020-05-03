using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync();
        Task<EventDto> GetByIdAsync(int id);
        Task<EventDto> AddNewEventAsync([FromBody] EventDto newEvent);
        Task UpdateEventAsync(int id, EventDto updateEvent);
        void DeleteAsync(int id);
    }
}

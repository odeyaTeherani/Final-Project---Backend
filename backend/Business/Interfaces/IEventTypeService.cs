using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IEventTypeService
    {
        Task<List<EventTypeDto>> GetAllAsync();
        Task<EventTypeDto> GetByIdAsync(int id);
        Task<EventTypeDto> AddNewEventTypeAsync([FromBody] string newEventType);
        Task<EventTypeDto> UpdateEventTypeAsync(int id, [FromBody] EventTypeDto updateEventType);
        Task DeleteAsync(int id);
    }
}

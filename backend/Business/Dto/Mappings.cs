// defines which object we agree to map to which object 
// if the fields are the same they are automatically copied, else we need to to this manual
using AutoMapper;
using backend.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Business.Dto
{
    public static class Mappings
    {
        public static MapperConfiguration InitMappings(this IServiceCollection serviceCollection)
        {
            // Event mapped to EventDto
            return new MapperConfiguration((cfg) =>
                {
                    cfg.CreateMap<Report, ReportDto>();
                    cfg.CreateMap<ReportDto, Report>();
                    cfg.CreateMap<EventType, EventTypeDto>();
                    cfg.CreateMap<EventTypeDto, EventType>();
                    cfg.CreateMap<Event, EventDto>();
                    cfg.CreateMap<EventDto, Event>();
                    //cfg.CreateMap<User, UserDto>();
                    //cfg.CreateMap<UserDto, User>();
                }
            );
        }
    }
}

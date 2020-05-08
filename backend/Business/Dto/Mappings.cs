// defines which object we agree to map to which object 
// if the fields are the same they are automatically copied, else we need to to this manual

using System;
using System.Linq;
using AutoMapper;
using backend.Business.Dto.ReportDtoModels;
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

                    cfg.CreateMap<AddReportDto, Report>()
                        .ForMember(x => x.Images,
                            opt => opt.Ignore())
                        .AfterMap((reportDto, report) =>
                        {
                            report.Images.AddRange(reportDto.Images.Select(imageData => new Image
                                {
                                    Id = Guid.NewGuid(),
                                    ImageData = imageData
                                }).ToList()
                            );
                        });


                    cfg.CreateMap<Report, GetReportDto>()
                        .ForMember(x => x.Images,
                            opt => opt.Ignore())
                        .ForMember(x => x.EventType,
                            opt => opt.Ignore())
                        .AfterMap((report, reportDto) =>
                        {
                            reportDto.Images = report.Images.Select(image => image.ImageData).ToList();
                            reportDto.EventType = report.EventType?.Type;
                        });

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
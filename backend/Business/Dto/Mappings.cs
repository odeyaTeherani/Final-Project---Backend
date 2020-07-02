// defines which object we agree to map to which object 
// if the fields are the same they are automatically copied, else we need to to this manual

using System;
using System.Linq;
using AutoMapper;
using backend.Business.Dto.ReportDtoModels;
using backend.Business.Dto.UserDto;
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
                    // cfg.CreateMap<ReportDto, Report>();


                    cfg.CreateMap<ReportDto, Report>()
                        .ForMember(x => x.Images,
                            opt => opt.Ignore())
                        .ForMember(x => x.EventType,
                            opt => opt.Ignore())
                        .AfterMap((reportDto, report) =>
                        {
                            report.Images.AddRange(reportDto.Images.Select(imageData => new Image
                                {
                                    Id = Guid.NewGuid(),
                                    ImageData = imageData
                                }).ToList()
                            );
                            report.EventTypeId = reportDto.EventType.Id;
                        });


                    cfg.CreateMap<Report, GetReportDto>()
                        .ForMember(x => x.Images,
                            opt => opt.Ignore())
                        // .ForMember(x => x.EventType, opt => opt.Ignore())
                        .AfterMap((report, reportDto) =>
                        {
                            reportDto.Images = report.Images.Select(image => image.ImageData).ToList();
                            // reportDto.EventType.Type = report.EventType?.Type;
                        });

                    cfg.CreateMap<EventType, EventTypeDto>();
                    cfg.CreateMap<EventTypeDto, EventType>();

                    cfg.CreateMap<SubRole, SubRoleDto>();
                    cfg.CreateMap<SubRoleDto, SubRole>();

                    cfg.CreateMap<UserInformationDto, ApplicationUser>()
                        .ForMember(s => s.SubRole,
                            opt => opt.Ignore())
                        .AfterMap((subRoleDto, subRole) => 
                            { subRole.SubRoleId = subRoleDto.SubRole.Id; });

                    cfg.CreateMap<ApplicationUser, UserInformationDto>();

                    cfg.CreateMap<Event, EventDto>()
                        .ForMember(x => x.Images, opt => opt.Ignore())
                        .ForMember(x => x.StartTime, opt => opt.Ignore())
                        .ForMember(x => x.EndTime, opt => opt.Ignore())
                        .AfterMap((report, reportDto) =>
                        {
                            reportDto.Images = report.Images.Select(image => image.ImageData).ToList();
                            reportDto.StartTime = report.StartDate.ToString("HH:mm");
                            reportDto.EndTime = report.EndDate.ToString("HH:mm");
                        });
                    cfg.CreateMap<EventDto, Event>()
                        .ForMember(x => x.Images,
                            opt => opt.Ignore())
                        .ForMember(x => x.EventType,
                            opt => opt.Ignore())
                        .ForMember(x => x.Id,
                            opt => opt.Ignore())
                        .AfterMap((dto, e) =>
                        {
                            e.EventTypeId = dto.EventType.Id;
                            e.Images.AddRange(dto.Images.Select(imageData => new Image
                                {
                                    Id = Guid.NewGuid(),
                                    ImageData = imageData
                                }).ToList()
                            );

                            // format date
                            e.StartDate = MapDateAndTimeToDateTimeObject(dto.StartDate.Date, dto.StartTime);
                            e.EndDate = MapDateAndTimeToDateTimeObject(dto.EndDate.Date, dto.EndTime);
                        });

                    cfg.CreateMap<LocationDto, Location>()
                        .ForMember(x => x.GooglePlacesDbId, opt => opt.Ignore())
                        .ForMember(x => x.Name, opt => opt.Ignore())
                        .ForMember(x => x.FormattedAddress, opt => opt.Ignore())
                        .ForMember(x => x.GoogleLatitude, opt => opt.Ignore())
                        .ForMember(x => x.GoogleLongitude, opt => opt.Ignore())
                        .ForMember(x => x.PlaceId, opt => opt.Ignore())
                        .AfterMap((dto, loc) =>
                        {
                            if (dto.GooglePlacesData == null) return;
                            loc.GooglePlacesDbId = dto.GooglePlacesData.GooglePlacesDbId;
                            loc.Name = dto.GooglePlacesData.Name;
                            loc.FormattedAddress = dto.GooglePlacesData.FormattedAddress;
                            loc.GoogleLatitude = dto.GooglePlacesData.GoogleLatitude;
                            loc.GoogleLongitude = dto.GooglePlacesData.GoogleLongitude;
                            loc.PlaceId = dto.GooglePlacesData.PlaceId;
                        });

                    cfg.CreateMap<Location, LocationDto>()
                        .ForMember(x => x.GooglePlacesData, x => x.Ignore())
                        .AfterMap((loc, dto) =>
                        {
                            dto.GooglePlacesData = new GooglePlacesData
                            {
                                Name = loc.Name,
                                FormattedAddress = loc.FormattedAddress,
                                GoogleLatitude = loc.GoogleLatitude,
                                GoogleLongitude = loc.GoogleLongitude,
                                PlaceId = loc.PlaceId,
                                GooglePlacesDbId = loc.GooglePlacesDbId
                            };
                        });

                    cfg.CreateMap<ApplicationUser, GetBasicUserInformation>();
                    //cfg.CreateMap<UserDto, User>();
                }
            );
        }

        private static DateTime MapDateAndTimeToDateTimeObject(DateTime date, string time)
        {
            var resultStatus = DateTime.TryParse(date.ToString("yyyy-MM-dd") + " " + time, out var result);
            if (!resultStatus)
            {
                Console.WriteLine($"date {date} with time {time} is not valid");
            }

            return result;
        }
    }
}
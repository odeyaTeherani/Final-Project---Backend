using System;
using backend.Business.Dto.UserDto;

namespace backend.Business.Dto.ReportDtoModels
{
    public class GetReportDto : ReportDto
    {

        public GetBasicUserInformation User { get; set; }
        public DateTime Date { get; set; }

    }
}
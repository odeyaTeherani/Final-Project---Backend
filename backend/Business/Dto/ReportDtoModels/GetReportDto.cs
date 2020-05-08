using System;

namespace backend.Business.Dto.ReportDtoModels
{
    public class GetReportDto : ReportDto
    {

        public string Name { get; set; } // reporter's name
        public string EventType { get; set; }
        public DateTime Date { get; set; }

    }
}
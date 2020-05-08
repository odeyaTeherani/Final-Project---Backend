namespace backend.Business.Dto.ReportDtoModels
{
    public class GetReportDto : ReportDto
    {

        public string Name { get; set; } // reporter's name
        public EventTypeDto EventType { get; set; }

    }
}
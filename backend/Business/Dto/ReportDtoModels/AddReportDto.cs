using Newtonsoft.Json;

namespace backend.Business.Dto.ReportDtoModels
{
    public class AddReportDto : ReportDto
    {
        [JsonProperty("eventType")]
        public int EventTypeId { get; set; } 
        
    }
}
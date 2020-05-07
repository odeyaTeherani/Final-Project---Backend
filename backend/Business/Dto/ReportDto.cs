// Data Transfer Object - the object that we want to sent to the internet - that we want will see heme in the internet from the Report
// Separation - here we
using System;
using backend.Data.Enums;
using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class ReportDto
    {

        public int Id { get; set; } 
        public EventTypeDto EventType { get; set; }
        
        [JsonProperty("severityLevel")]
        public SeverityLevel SeverityLevelType { get; set; }
        // public Location Location { get; set; }
        public DateTime Date { get; set; }
        
        
        public string CarNumber { get; set; }
        public string Name { get; set; } // reporter's name
        public string Note { get; set; }
        
        [JsonProperty("numberOfEvacuatedInjured")]
        public int Casualties { get; set; } // num of casualties

    }
}

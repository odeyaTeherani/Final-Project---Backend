// Data Transfer Object - the object that we want to sent to the internet - that we want will see heme in the internet from the Report
// Separation - here we

using System;
using System.Collections.Generic;
using backend.Data.Enums;
using Newtonsoft.Json;

namespace backend.Business.Dto.ReportDtoModels
{
    public class ReportDto
    {
        public int Id { get; set; }
        
        [JsonProperty("severityLevel")]
        public SeverityLevel SeverityLevelType { get; set; }
        // public Location Location { get; set; }
    
        public string CarNumber { get; set; }

        public string Note { get; set; }
        
        [JsonProperty("numberOfEvacuatedInjured")]
        public int Casualties { get; set; } // num of casualties

        public List<string> Images { get; set; }
        
                
        [JsonProperty("location")]
        public LocationDto Location { get; set; }

    }
}

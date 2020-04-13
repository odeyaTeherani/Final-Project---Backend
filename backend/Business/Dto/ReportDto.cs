// Data Transfer Object - the object that we want to sent to the internet - that we want will see heme in the internet from the Report
// Separation - here we
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class ReportDto
    {

        public int Id { get; set; }
        public string EventType { get; set; }
        public string SeverityLevel { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        public string CarNumber { get; set; }

        [JsonProperty("eventName")] // sending the name in another name
        public string Name { get; set; }
        public string Note { get; set; }
        public int NumOfEv { get; set; }
        // image

    }
}

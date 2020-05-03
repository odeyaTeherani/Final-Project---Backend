// Data Transfer Object - the object that we want to sent to the internet - that we want will see heme in the internet from the Report
// Separation - here we
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Enums;
using backend.Data.Models;
using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class ReportDto
    {

        public int Id { get; set; }
        public EventType EventType { get; set; }
        public SeverityLevel SeverityLevelType { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public string CarNumber { get; set; }
        /// <summary>
        /// represent the current connected user
        /// </summary>
        [JsonProperty("eventName")] // sending the name in another name
        public string Name { get; set; } // reporter's name
        public string Note { get; set; }
        public int Casualties { get; set; } // num of casualties
        public List<Image> Images { get; set; }
    }
}

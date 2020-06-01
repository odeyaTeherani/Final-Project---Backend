using System;
using System.Collections.Generic;
using backend.Business.Dto.ReportDtoModels;
using backend.Data.Enums;
using backend.Data.Models;
using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class EventDto
    {
        public int? Id { get; set; } //primary key
        
        public EventTypeDto EventType { get; set; }
        
        // public Location Location { get; set; }
        
        [JsonProperty("severityLevel")]
        public SeverityLevel SeverityLevelType { get; set; }
        
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }
        
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
        
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        
        [JsonProperty("numOfInjured")]
        public int NumOfInjured { get; set; }
        
        [JsonProperty("numOfDead")]
        public int NumOfDead { get; set; }
        
        [JsonProperty("numOfPolice")]
        public int NumOfPolice { get; set; }
        
        [JsonProperty("numOfAmbulances")]
        public int NumOfAmbulances { get; set; }
        
        [JsonProperty("numOfFirefighters")]
        public int NumOfFirefighters { get; set; }
        
        [JsonProperty("numOfEnvironment")]
        public int NumOfEnvironment { get; set; }
        
        [JsonProperty("numOfZakaCars")]
        public int NumOfZakaCars { get; set; }
        
        [JsonProperty("nameInCharge")]
        public string NameInCharge { get; set; }        
        
        [JsonProperty("note")]
        public string Note { get; set; }
        public List<ReportDto> Reports { get; set; }
    }
}

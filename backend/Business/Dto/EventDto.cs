using System;
using System.Collections.Generic;
using backend.Data.Enums;
using backend.Data.Models;

namespace backend.Business.Dto
{
    public class EventDto
    {
        public int Id { get; set; } //primary key
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
        //public Location Location { get; set; }
        public SeverityLevel SeverityLevelType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public int NumOfInjured { get; set; }
        public int NumOfDead { get; set; }
        public int NumOfPolice { get; set; }
        public int NumOfAmbulances { get; set; }
        public int NumOfFirefighters { get; set; }
        public int NumOfEnvironment { get; set; }
        public int NumOfZakaCars { get; set; }
        public string NameInCharge { get; set; }
        public List<Report> Reports { get; set; }
        public List<string> Images { get; set; }
    }
}

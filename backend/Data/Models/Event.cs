using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;

namespace backend.Data.Models
{
    [Table("Events")]
    public class Event
    {
        public Event()
        {
            Images = new List<Image>();
        }

        [Key] // "EventId" is primary key (uniq)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The "EventId" will be managed automatically
        public int Id { get; set; } //primary key

        [ForeignKey("EventTypeId")]
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }

        public SeverityLevel SeverityLevelType { get; set; }

        [ForeignKey("LocationId")]
        public int? LocationId { get; set; }
        public Location Location { get; set; }

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
        public List<Image> Images { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;


namespace backend.Data.Models
{
    [Table("Reports")]

    public class Report
    {
        public Report()
        {
            Images = new List<Image>();    
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("EventTypeId")]
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
        
        public SeverityLevel SeverityLevelType { get; set; }

        [ForeignKey("LocationId")]
        public int? LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime Date { get; set; }
        public string CarNumber { get; set; }

        [MaxLength(200)]
        public string Note { get; set; }
        public int Casualties { get; set; } // num of casualties
        public List<Image> Images { get; set; }
        
        
        [ForeignKey("EventId")]
        public int? EventId { get; set; }
        public Event Event { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Enums;

namespace backend.Data.Models
{
    [Table("Events")]
    public class Event
    {
        [Key] // "EventId" is primary key (uniq)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The "EventId" will be managed automatically
        public int Id { get; set; } //primary key
        public EventType EventType { get; set; }
        public List<Report> Reports { get; set; }
        public SeverityLevel SeverityLevelType { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public List<Image> Images { get; set; }
    }
}

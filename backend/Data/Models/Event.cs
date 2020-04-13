using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Models
{
    [Table("Events")]
    public class Event
    {
        [Key] // "EventId" is primary key (uniq)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The "EventId" will be managed automatically
        public int Id { get; set; } //primary key
        public string EventType { get; set; }
        public string SeverityLevel { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}

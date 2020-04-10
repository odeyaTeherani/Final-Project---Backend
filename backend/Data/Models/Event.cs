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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EventType { get; set; }
        public string SeverityLevel { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

    }
}

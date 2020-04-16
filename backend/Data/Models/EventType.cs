using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Models
{
    public class EventType
    {
        [Key] // "Id" is primary key (uniq)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The "Id" will be managed automatically
        public int Id { get; set; } //primary 
        public string Type { get; set; } //Type of the event
    }
}

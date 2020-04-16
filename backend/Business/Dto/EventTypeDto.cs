using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Business.Dto
{
    public class EventTypeDto
    {
        [Key] // "Id" is primary key (uniq)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The "Id" will be managed automatically
        public int Id { get; set; } //primary 
        public string Type { get; set; } //Type of the event
    }
}

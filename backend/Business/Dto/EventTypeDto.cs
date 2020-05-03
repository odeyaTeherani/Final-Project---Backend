using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Business.Dto
{
    public class EventTypeDto
    {
        public int Id { get; set; } //primary 
        public string Type { get; set; } //Type of the event
    }
}

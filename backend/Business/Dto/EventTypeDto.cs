using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class EventTypeDto
    {
        public int Id { get; set; } //primary 
        
        [JsonProperty("name")]
        public string Type { get; set; } //Type of the event
    }
}

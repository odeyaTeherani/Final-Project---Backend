using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class SubRoleDto
    {
        public int Id { get; set; }
        
        [JsonProperty("subRole")]
        public string Name { get; set; }
    }
}
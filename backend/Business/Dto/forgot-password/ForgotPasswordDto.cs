using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class ForgotPasswordDto
    {
        [Required] 
        [JsonProperty("email")]
        public string Email { get; set; }
        
    }
}
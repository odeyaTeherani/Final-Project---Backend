using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        [JsonProperty("currentPassword")]
        public string CurrentPassword { get; set; }

        [Required]
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
        
        [JsonProperty("confirmNewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}

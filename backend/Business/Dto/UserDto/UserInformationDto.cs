using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace backend.Business.Dto.UserDto
{
    public class UserInformationDto
    {
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("role")]
        public string Role { get; set; }
        
        [JsonProperty("subRole")]
        public SubRoleDto SubRole { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User name")]
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [JsonProperty("email")]
        public string Email { get; set; }


        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        
        [JsonProperty("image")]
        public string Image { get; set; }

    }
}

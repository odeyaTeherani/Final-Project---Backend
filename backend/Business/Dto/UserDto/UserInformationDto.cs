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
        
        public string SubRole { get; set; }

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

        // [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [JsonProperty("password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
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

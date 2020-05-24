using System.ComponentModel.DataAnnotations;

namespace backend.Business.Dto.UserDto
{
    public class LoginDto
    {
        [Required] 
        public string Username { get; set; }        
        [Required] 
        public string Password { get; set; }


    }
}

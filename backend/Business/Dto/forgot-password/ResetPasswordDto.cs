using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class ResetPasswordDto
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}

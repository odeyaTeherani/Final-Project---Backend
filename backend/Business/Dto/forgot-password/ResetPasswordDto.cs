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
        
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }
}

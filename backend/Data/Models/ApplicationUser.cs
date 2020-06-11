using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        // public IList<string> Role { get; set; }
        //
        public string SubRole { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

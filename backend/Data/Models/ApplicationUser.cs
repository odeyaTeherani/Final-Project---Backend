using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("SubRoleId")]
        public int SubRoleId { get; set; }
        public SubRole SubRole { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

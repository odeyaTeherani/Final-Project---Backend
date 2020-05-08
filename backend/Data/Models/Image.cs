using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Models
{
    public class Image
    {
        [Key] // "Id" is primary key (uniq)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The "Id" will be managed automatically
        public Guid Id { get; set; } //primary key

        // in the future 
        public string ImageUrl { get; set; }
        // Meta Data
        public string ImageSize { get; set; } //Size of Image
        //publish date time
        public string ImageData { get; set; }
    }
}

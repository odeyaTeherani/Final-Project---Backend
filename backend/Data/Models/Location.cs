using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data.Models
{
    [Table("Locations")]
    public class Location
    {
        [Key] // "Id" is primary key (uniq)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // The "Id" will be managed automatically
        public int Id { get; set; } //primary key
        
        /// <summary>
        ///  Sensors Location
        /// </summary>
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        
        /// <summary>
        /// Google places api location data
        /// </summary>
        public string FormattedAddress { get; set; }
        public double? GoogleLatitude { get; set; }
        public double? GoogleLongitude { get; set; }
        public string GooglePlacesDbId { get; set; }
        public string Name { get; set; }
        public string PlaceId { get; set; }
    }
}

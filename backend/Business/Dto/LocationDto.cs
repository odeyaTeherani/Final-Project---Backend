using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class LocationDto
    {
        public GooglePlacesData GooglePlacesData { get; set; } = null;

        /// <summary>
        ///  Sensors location
        /// </summary>
        public double? Longitude { get; set; }

        public double? Latitude { get; set; }
    }

    public class GooglePlacesData
    {
        public string FormattedAddress { get; set; }
        [JsonProperty("lat")] 
        public double? GoogleLatitude { get; set; }
        [JsonProperty("lng")] 
        public double? GoogleLongitude { get; set; }
        public string GooglePlacesDbId { get; set; }
        public string Name { get; set; }
        public string PlaceId { get; set; }
    }
}
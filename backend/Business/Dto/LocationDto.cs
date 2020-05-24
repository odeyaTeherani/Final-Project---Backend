using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Business.Dto
{
    public class LocationDto
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}

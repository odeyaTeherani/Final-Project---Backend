using Newtonsoft.Json;

namespace backend.Business.Dto
{
    public class ConsultDto
    {
        [JsonProperty("number_of_police_cars")]
        public string PoliceCars { get; set; }
        
        [JsonProperty("number_of_fire_fighters_car")]
        public string FireFightersCars { get; set; }
        
        [JsonProperty("number_of_ambulances_car")]
        public string AmbulancesCars { get; set; }
        
        [JsonProperty("number_of_zaka_cars")]
        public string ZakaCars { get; set; }
        
        [JsonProperty("number_of_environment_car")]
        public string EnvironmentCars { get; set; }

        public override string ToString()
        {
            return $"Number of Police cars: {PoliceCars}, \n" +
                   $"Number of Fire Fighters car: {FireFightersCars}, \n" +
                   $"Number of Ambulances car: {AmbulancesCars}, \n" +
                   $"Number of Zaka cars: {ZakaCars}, \n" +
                   $"Number of Environment cars: {EnvironmentCars} \n";
        }
    }
}
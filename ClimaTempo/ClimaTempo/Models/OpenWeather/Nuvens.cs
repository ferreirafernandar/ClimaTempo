using Newtonsoft.Json;

namespace ClimaTempo.Models.OpenWeather
{
    public class Nuvens
    {
        [JsonProperty("All")]
        public int Total { get; set; }
    }
}
using Newtonsoft.Json;

namespace ClimaTempo.Models.OpenWeather
{
    public class Vento
    {
        [JsonProperty("Speed")]
        public double Velocidade { get; set; }
        public int Deg { get; set; }
    }
}
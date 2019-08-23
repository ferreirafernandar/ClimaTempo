using Newtonsoft.Json;

namespace ClimaTempo.Models.OpenWeather
{
    public class Principal
    {
        [JsonProperty("Pressure")]
        public int Pressao { get; set; }

        [JsonProperty("Humidity")]
        public int Umidade { get; set; }

        [JsonProperty("Temp")]
        public double Temperatura { get; set; }

        [JsonProperty("Temp_min")]
        public double TemperaturaMinima { get; set; }

        [JsonProperty("Temp_max")]
        public double TemperaturaMaxima { get; set; }
    }
}
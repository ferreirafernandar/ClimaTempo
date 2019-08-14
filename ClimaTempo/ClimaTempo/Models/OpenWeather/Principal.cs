using Newtonsoft.Json;

namespace ClimaTempo.Models.OpenWeather
{
    public class Principal
    {
        [JsonProperty("Temp")]
        public double Temperatura { get; set; }

        [JsonProperty("Pressure")]
        public int Pressao { get; set; }

        [JsonProperty("Humidity")]
        public int Umidade { get; set; }

        [JsonProperty("Temp_min")]
        public int TemperaturaMinima { get; set; }

        [JsonProperty("Temp_max")]
        public int TemperaturaMaxima { get; set; }
    }
}
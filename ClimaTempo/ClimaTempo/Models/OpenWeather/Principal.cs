using Newtonsoft.Json;

namespace ClimaTempo.Models.OpenWeather
{
    public class Principal
    {
        private string _temperatura;
        private string _temperaturaMinima;
        private string _temperaturaMaxima;

        [JsonProperty("Temp")]
        public string Temperatura
        {
            get => $"{_temperatura}°";
            set => _temperatura = value;
        }

        [JsonProperty("Pressure")]
        public int Pressao { get; set; }

        [JsonProperty("Humidity")]
        public int Umidade { get; set; }

        [JsonProperty("Temp_min")]
        public string TemperaturaMinima
        {
            get => $"{_temperaturaMinima}°";
            set => _temperaturaMinima = value;
        }

        [JsonProperty("Temp_max")]
        public string TemperaturaMaxima
        {
            get => $"{_temperaturaMaxima}°";
            set => _temperaturaMaxima = value;
        }
    }
}
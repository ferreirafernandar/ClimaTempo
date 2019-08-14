using Newtonsoft.Json;

namespace ClimaTempo.Models.OpenWeather
{
    public class Sys
    {
        [JsonProperty("Type")]
        public int Tipo { get; set; }
        public int Id { get; set; }

        [JsonProperty("Message")]
        public double Mensagem { get; set; }

        [JsonProperty("Country")]
        public string Pais { get; set; }

        [JsonProperty("Sunrise")]
        public int NascerSol { get; set; }

        [JsonProperty("Sunset")]
        public int PorSol { get; set; }
    }
}
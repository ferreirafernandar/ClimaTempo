using Newtonsoft.Json;

namespace ClimaTempo.Models.OpenWeather
{
    public class Clima
    {
        public int Id { get; set; }

        [JsonProperty("Main")]
        public string Principal { get; set; }

        [JsonProperty("Description")]
        public string Descricao { get; set; }

        [JsonProperty("Icon")]
        public string Icone { get; set; }
    }
}
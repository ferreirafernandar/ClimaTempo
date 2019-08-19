using Newtonsoft.Json;

namespace ClimaTempo.Models.OpenWeather
{
    public class Clima
    {
        private string _icone;
        public int Id { get; set; }

        [JsonProperty("Main")]
        public string Principal { get; set; }

        [JsonProperty("Description")]
        public string Descricao { get; set; }

        [JsonProperty("Icon")]
        public string Icone
        {
            get => $"icon_{_icone}.png";
            set => _icone = value;
        }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClimaTempo.Models.OpenWeather
{
    public class ClimaAtual
    {
        [JsonProperty("Coord")]
        public Coordenada Coordenada { get; set; }

        [JsonProperty("Weather")]
        public List<Clima> Clima { get; set; }

        [JsonProperty("Base")]
        public string Base { get; set; }

        [JsonProperty("Main")]
        public Principal Principal { get; set; }

        [JsonProperty("Visibility")]
        public int Visibilidade { get; set; }

        [JsonProperty("Wind")]
        public Vento Vento { get; set; }

        [JsonProperty("Clouds")]
        public Nuvens Nuvens { get; set; }

        public int Dt { get; set; }
        public Sys Sys { get; set; }

        [JsonProperty("Timezone")]
        public int FusoHorario { get; set; }

        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Nome { get; set; }

        [JsonProperty("Cod")]
        public int Codigo { get; set; }
    }
}
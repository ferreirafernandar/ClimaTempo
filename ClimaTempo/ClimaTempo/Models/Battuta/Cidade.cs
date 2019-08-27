using Newtonsoft.Json;

namespace ClimaTempo.Models.Battuta
{
    public class Cidade 
    {
        [JsonProperty("City")]
        public string NomeCidade { get; set; }

        [JsonProperty("Region")]
        public string Estado { get; set; }

        [JsonProperty("Country")]
        public string Pais { get; set; }
    }
}
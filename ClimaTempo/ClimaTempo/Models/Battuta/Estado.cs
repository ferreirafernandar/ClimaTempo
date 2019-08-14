using Newtonsoft.Json;

namespace ClimaTempo.Models.Battuta
{
    public class Estado
    {
        [JsonProperty("Region")]
        public string NomeEstado { get; set; }

        [JsonProperty("Country")]
        public string Pais { get; set; }
    }
}
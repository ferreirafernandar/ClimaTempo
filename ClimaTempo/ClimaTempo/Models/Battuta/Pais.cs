using Newtonsoft.Json;

namespace ClimaTempo.Models.Battuta
{
    public class Pais
    {
        [JsonProperty("Name")]
        public string NomePais { get; set; }

        [JsonProperty("Code")]
        public string Codigo { get; set; }
    }
}
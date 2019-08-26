using Newtonsoft.Json;

namespace ClimaTempo.Models.AppCenter
{
    public class ConteudoNotificacao
    {
        [JsonProperty("name")]
        public string Nome { get; set; }

        [JsonProperty("title")]
        public string Titulo { get; set; }

        [JsonProperty("body")]
        public string Conteudo { get; set; }
    }
}
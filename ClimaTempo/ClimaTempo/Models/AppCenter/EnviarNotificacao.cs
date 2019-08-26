using Newtonsoft.Json;

namespace ClimaTempo.Models.AppCenter
{
    public class EnviarNotificacao
    {
        [JsonProperty("notification_target")]
        public ObjetivoNotificacao ObjetivoNotificacao { get; set; }

        [JsonProperty("notification_content")]
        public ConteudoNotificacao ConteudoNotificacao { get; set; }
    }
}
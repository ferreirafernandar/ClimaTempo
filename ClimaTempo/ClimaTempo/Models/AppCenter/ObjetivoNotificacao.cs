using System;
using Newtonsoft.Json;

namespace ClimaTempo.Models.AppCenter
{
    public class ObjetivoNotificacao
    {
        [JsonProperty("type")] 
        public string Tipo { get; set; } = "devices_target";

        [JsonProperty("devices")]
        public Guid[] Dispositivos { get; set; }
    }
}
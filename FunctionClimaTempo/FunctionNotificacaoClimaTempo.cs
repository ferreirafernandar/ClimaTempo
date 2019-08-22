using ClimaTempo.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunctionClimaTempo
{
    public static class FunctionNotificacaoClimaTempo
    {

        [FunctionName("NotificacoesClimaTempo")]
        //public static async System.Threading.Tasks.Task Run([TimerTrigger("0 00 6 * * *")]TimerInfo myTimer, ILogger log)
        public static async System.Threading.Tasks.Task<IActionResult> Run([HttpTrigger]HttpRequest req, ILogger log)

        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var firebaseService = new FirebaseService();

            var openWeatherService = new OpenWeatherService(new HttpClient());

            var notificacoes = await firebaseService.ObterNotificacoes();

            var stringBuilder = new StringBuilder();

            foreach (var notificacao in notificacoes)
            {
               var cidade = await openWeatherService.ObterClimaTempo(notificacao.Cidade);

               if (notificacao.DeveEnviarNotificacaoDeTemperatura(Convert.ToDouble(cidade.Principal.TemperaturaMinima)))
               {
                   stringBuilder.AppendLine(
                       $"Cidade {notificacao.Cidade} com temperatura atual {cidade.Principal.TemperaturaMinima} menor que {notificacao.TemperaturaMinima}");
               }

               if (notificacao.DeveEnviarNotificacaoDeVento(Convert.ToDouble(cidade.Vento.Velocidade)))
               {
                   stringBuilder.AppendLine(
                       $"Cidade {notificacao.Cidade} com velocidade atual do vendo {cidade.Vento.Velocidade} menor que {notificacao.VentoMinimo}");
               }
            }

            return new OkObjectResult(stringBuilder.ToString());
        }
    }
}

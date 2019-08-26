using ClimaTempo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

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
            var appCenterService = new AppCenterService(new HttpClient());

            var openWeatherService = new OpenWeatherService(new HttpClient());

            var notificacoes = await firebaseService.ObterNotificacoes();

            foreach (var notificacao in notificacoes)
            {
               var cidade = await openWeatherService.ObterClimaTempo(notificacao.Cidade);

               if (notificacao.DeveNotificarTemperaturaMinima(cidade.Principal.TemperaturaMinima))
               {
                   await appCenterService.AdicionarNotificacao(
                       new[] {Guid.Parse("b4bfe9a8-3762-4cb3-917c-f086620205a0")}, "ClimaTempo",
                       "A temperatura caiu!",
                       $"Cidade {notificacao.Cidade} com temperatura atual {cidade.Principal.TemperaturaMinima} menor que {notificacao.TemperaturaMinima}");
               }

               if (notificacao.DeveNotificarVentoVelocidadeMinima(Convert.ToDouble(cidade.Vento.Velocidade)))
               {
                   await appCenterService.AdicionarNotificacao(
                       new[] {Guid.Parse("b4bfe9a8-3762-4cb3-917c-f086620205a0")}, "ClimaTempo",
                       "A velocidade do vendo caiu!",
                       $"Cidade {notificacao.Cidade} com velocidade atual do vendo {cidade.Vento.Velocidade} menor que {notificacao.VentoMinimo}");
               }
            }

            return new OkObjectResult("OK");
        }
    }
}

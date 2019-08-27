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
                        new[] { notificacao.IdDispositivo }, "ClimaTempo",
                        "A temperatura caiu!",
                        $"Cidade de {notificacao.Cidade} com temperatura atual de {cidade.Principal.TemperaturaMinima}°.");
                }

                if (notificacao.DeveNotificarVentoVelocidadeMinima(Convert.ToDouble(cidade.Vento.Velocidade)))
                {
                    await appCenterService.AdicionarNotificacao(
                        new[] { notificacao.IdDispositivo }, "ClimaTempo",
                        "A velocidade do vendo caiu!",
                        $"Cidade de {notificacao.Cidade} com velocidade atual do vendo em {cidade.Vento.Velocidade}.");
                }
            }

            return new OkObjectResult("OK");
        }
    }
}

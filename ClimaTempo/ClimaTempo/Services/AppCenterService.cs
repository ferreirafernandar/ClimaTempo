using ClimaTempo.Models.AppCenter;
using ClimaTempo.Services.Interfaces;
using ClimaTempo.Utils;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    public class AppCenterService : IAppCenterService
    {
        private readonly IHttpClient _httpClient;
        private static Task<Uri> _enviarNotificacao;

        public AppCenterService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<Uri> AdicionarBaseHttpClient<T>(string endpoint, object model)
        {
            return _httpClient.AdicionarBaseHttpClient<T>(Configuracoes.UrlBaseAppCenter, endpoint, model, Configuracoes.AppCenterKey);
        }

        public Task<Uri> AdicionarNotificacao(Guid[] devices, string nome, string titulo, string conteudo)
        {
            var adicionarNotificacao = new EnviarNotificacao
            {
                ConteudoNotificacao = new ConteudoNotificacao
                {
                    Conteudo = conteudo, 
                    Nome = nome,
                    Titulo = titulo
                },
                ObjetivoNotificacao = new ObjetivoNotificacao
                {
                    Dispositivos = devices
                }
            };

            _enviarNotificacao = AdicionarBaseHttpClient<EnviarNotificacao>("v0.1/apps/ferreirafernandar/ClimaTempo/push/notifications", adicionarNotificacao);

            return _enviarNotificacao;
        }
    }
}
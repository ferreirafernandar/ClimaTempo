﻿using ClimaTempo.Models.AppCenter;
using ClimaTempo.Services.Interfaces;
using ClimaTempo.Utils;
using System;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    public class AppCenterService : IAppCenterService
    {
        private readonly IHttpClient _httpClient;

        public AppCenterService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<T> AdicionarBaseHttpClient<T>(string endpoint, object model)
        {
            return _httpClient.AdicionarBaseHttpClient<T>(Configuracoes.UrlBaseAppCenter, endpoint, model, Configuracoes.AppCenterKey);
        }

        public Task<EnviarNotificacao> AdicionarNotificacao(Guid[] devices, string nome, string titulo, string conteudo)
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

            return AdicionarBaseHttpClient<EnviarNotificacao>("v0.1/apps/ferreirafernandar/ClimaTempo/push/notifications", adicionarNotificacao);
        }
    }
}
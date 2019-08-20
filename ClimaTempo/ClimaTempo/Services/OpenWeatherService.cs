using System;
using System.Threading.Tasks;
using ClimaTempo.Models.OpenWeather;
using ClimaTempo.Services.Interfaces;
using ClimaTempo.Utils;
using Xamarin.Essentials;

namespace ClimaTempo.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClient _httpClient;
        private static Task<ClimaAtual> _climaTempo;

        public OpenWeatherService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<T> ObterBaseHttpClient<T>(string endpoint, params string[] parameters)
        {
            return _httpClient.ObterBaseHttpClient<T>(Configuracoes.UrlBaseOpenWeather, endpoint, parameters);
        }

        public Task<ClimaAtual> ObterClimaTempo(string cidade)
        {
            if (_climaTempo != null)
                return _climaTempo;

            _climaTempo = ObterBaseHttpClient<ClimaAtual>("weather", $"q={cidade}", $"appid={Configuracoes.OpenWeatherKey}", "units=metric");

            return _climaTempo;
        }
        
        public void GerarIdDispositivo()
        {
            try
            {
                var idDispositivo = ObterIdDispositivo().Result;
                if (idDispositivo == null)
                    SecureStorage.SetAsync("token", Guid.NewGuid().ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> ObterIdDispositivo()
        {
            try
            {
                return await SecureStorage.GetAsync("token");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
using ClimaTempo.Models.OpenWeather;
using ClimaTempo.Services.Interfaces;
using ClimaTempo.Utils;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IHttpClient _httpClient;
        
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
            return ObterBaseHttpClient<ClimaAtual>("weather", $"q={cidade}", $"appid={Configuracoes.OpenWeatherKey}", "units=metric");
        }
    }
}
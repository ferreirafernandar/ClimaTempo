using System.Threading.Tasks;
using ClimaTempo.Models.OpenWeather;
using ClimaTempo.Services.Interfaces;
using ClimaTempo.Utils;

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
            return _httpClient.ObterBaseHttpClient<T>(Singleton.UrlBaseOpenWeather, endpoint, parameters);
        }

        public Task<ClimaAtual> ObterClimaTempo(string cidade)
        {
            if (_climaTempo != null)
                return _climaTempo;

            _climaTempo = ObterBaseHttpClient<ClimaAtual>("weather", $"q={cidade}", $"appid={Singleton.OpenWeatherKey}", "units=metric");

            return _climaTempo;
        }
    }
}
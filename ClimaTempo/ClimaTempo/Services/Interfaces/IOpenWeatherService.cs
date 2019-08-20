using System.Threading.Tasks;
using ClimaTempo.Models.OpenWeather;

namespace ClimaTempo.Services.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<ClimaAtual> ObterClimaTempo(string cidade);
        void GerarIdDispositivo();
        Task<string> ObterIdDispositivo();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ClimaTempo.Models;
using ClimaTempo.Models.Battuta;
using ClimaTempo.Services.Interfaces;
using ClimaTempo.Utils;

namespace ClimaTempo.Services
{
    public class BattutaService : IBattutaService
    {
        private readonly IHttpClient _httpClient;

        public BattutaService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<T> ObterBaseHttpClient<T>(string endpoint, params string[] parameters)
        {
            return _httpClient.ObterBaseHttpClient<T>(Configuracoes.UrlBaseBattuta, endpoint, parameters);
        }

        public  Task<IEnumerable<Pais>> ObterPaises()
        {
            return ObterBaseHttpClient<IEnumerable<Pais>>("country/all/", $"key={Configuracoes.BattutaKey}");
        }

        public Task<IEnumerable<Estado>> ObterEstados(string codigoPais)
        {
            return ObterBaseHttpClient<IEnumerable<Estado>>($"region/{codigoPais}/all/", $"key={Configuracoes.BattutaKey}");
        }

        public Task<IEnumerable<Cidade>> ObterCidades(string codigoPais, string estado)
        {
            return ObterBaseHttpClient<IEnumerable<Cidade>>($"city/{codigoPais}/search/", $"region={estado}", $"key={Configuracoes.BattutaKey}");
        }
    }
}
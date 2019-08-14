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

        private static Task<IEnumerable<Pais>> _paises;
        private static Task<IEnumerable<Estado>> _estados;
        private static Task<IEnumerable<Cidade>> _cidades;

        public BattutaService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<T> ObterBaseHttpClient<T>(string endpoint, params string[] parameters)
        {
            return _httpClient.ObterBaseHttpClient<T>(Singleton.UrlBaseBattuta, endpoint, parameters);
        }

        public  Task<IEnumerable<Pais>> ObterPaises()
        {
            if (_paises != null)
                return _paises;

            _paises = ObterBaseHttpClient<IEnumerable<Pais>>("country/all/", $"key={Singleton.BattutaKey}");

            return _paises;
        }

        public Task<IEnumerable<Estado>> ObterEstados(string codigoPais)
        {
            if (_estados != null)
                return _estados;

            _estados = ObterBaseHttpClient<IEnumerable<Estado>>($"region/{codigoPais}/all/", $"key={Singleton.BattutaKey}");

            return _estados;
        }

        public Task<IEnumerable<Cidade>> ObterCidades(string codigoPais, string estado)
        {
            if (_cidades != null)
                return _cidades;

            _cidades = ObterBaseHttpClient<IEnumerable<Cidade>>($"city/{codigoPais}/search/", $"region={estado}", $"key={Singleton.BattutaKey}");

            return _cidades;
        }
    }
}
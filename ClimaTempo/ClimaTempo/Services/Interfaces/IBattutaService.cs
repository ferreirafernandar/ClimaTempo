using System.Collections.Generic;
using System.Threading.Tasks;
using ClimaTempo.Models.Battuta;

namespace ClimaTempo.Services.Interfaces
{
    public interface IBattutaService
    {
        Task<IEnumerable<Pais>> ObterPaises();
        Task<IEnumerable<Estado>> ObterEstados(string codigoPais);
        Task<IEnumerable<Cidade>> ObterCidades(string codigoPais, string estado);
    }
}
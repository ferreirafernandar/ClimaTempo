using System;
using System.Threading.Tasks;
using ClimaTempo.Models.AppCenter;

namespace ClimaTempo.Services.Interfaces
{
    public interface IAppCenterService
    {   
        Task<EnviarNotificacao> AdicionarNotificacao(Guid[] devices, string nome, string titulo, string conteudo);
    }
}
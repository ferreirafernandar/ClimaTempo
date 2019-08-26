using System;
using System.Threading.Tasks;

namespace ClimaTempo.Services.Interfaces
{
    public interface IAppCenterService
    {
        Task<Uri> AdicionarNotificacao(Guid[] devices, string nome, string titulo, string conteudo);
    }
}
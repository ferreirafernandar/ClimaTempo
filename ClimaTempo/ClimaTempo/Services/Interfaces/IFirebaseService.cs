using ClimaTempo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClimaTempo.Services.Interfaces
{
    public interface IFirebaseService
    {
        Task<Notificacao> AdicionarNotificacao(Notificacao notificacao);
        Task<List<Notificacao>> ObterNotificacoes();
    }
}
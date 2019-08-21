using System.Threading.Tasks;
using ClimaTempo.Models;

namespace ClimaTempo.Services.Interfaces
{
    public interface IFirebaseService
    {
        Task<Notificacao> AdicionarNotificacao(Notificacao notificacao);
    }
}
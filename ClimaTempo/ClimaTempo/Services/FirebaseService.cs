using ClimaTempo.Models;
using ClimaTempo.Services.Interfaces;
using ClimaTempo.Utils;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseService(IOpenWeatherService openWeatherService)
        {
            _firebaseClient = new FirebaseClient(Configuracoes.UrlBaseFirebase);
        }

        public async Task<Notificacao> AdicionarNotificacao(Notificacao notificacao)
        {
            var resultado = await _firebaseClient
                                .Child("notificacoes")
                                .PostAsync(notificacao);

            return resultado?.Object;
        }
    }
}
using ClimaTempo.Models;
using ClimaTempo.Services.Interfaces;
using ClimaTempo.Utils;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseService()
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

        public async Task<List<Notificacao>> ObterNotificacoes()
        {
            return (await _firebaseClient
                    .Child("notificacoes")
                    .OnceAsync<Notificacao>()).Select(item => new Notificacao
                    {
                        Cidade = item.Object.Cidade,
                        IdDispositivo = item.Object.IdDispositivo,
                        TemperaturaMinima = item.Object.TemperaturaMinima,
                        Chuva = item.Object.Chuva,
                        VentoMinimo = item.Object.VentoMinimo
                    }).ToList();
        }
    }
}
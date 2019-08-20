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
        private readonly IOpenWeatherService _openWeatherService;

        public FirebaseService(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
            _firebaseClient = new FirebaseClient(Configuracoes.UrlBaseFirebase);
        }

        public async Task AdicionarNotificacao(Notificacao notificacao)
        {
            await _firebaseClient
                                .Child("notificacoes")
                                .PostAsync(new Notificacao
                                {
                                    IdDispositivo = _openWeatherService.ObterIdDispositivo().Result,
                                    Cidade = "Salvador",
                                    TemperaturaMinima = "20",
                                    VentoMinimo = "2.5",
                                    Chuva = false
                                });
        }
    }
}
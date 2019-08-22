namespace ClimaTempo.Models
{
    public class Notificacao
    {
        public string IdDispositivo { get; set; }
        public string Cidade { get; set; }
        public double TemperaturaMinima { get; set; }
        public double VentoMinimo { get; set; }
        public bool Chuva { get; set; }

        public bool DeveEnviarNotificacaoDeTemperatura(double temperaturaAtual)
            => temperaturaAtual < TemperaturaMinima;

        public bool DeveEnviarNotificacaoDeVento(double ventoAtual)
            => ventoAtual < VentoMinimo;
    }
}
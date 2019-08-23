namespace ClimaTempo.Models
{
    public class Notificacao
    {
        public string IdDispositivo { get; set; }
        public string Cidade { get; set; }
        public double TemperaturaMinima { get; set; }
        public double VentoMinimo { get; set; }
        public bool Chuva { get; set; }

        public bool DeveNotificarTemperaturaMinima(double temperaturaAtual)
            => temperaturaAtual < TemperaturaMinima;

        public bool DeveNotificarVentoVelocidadeMinima(double ventoAtual)
            => ventoAtual < VentoMinimo;
    }
}
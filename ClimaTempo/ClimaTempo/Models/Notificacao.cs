namespace ClimaTempo.Models
{
    public class Notificacao
    {
        public string IdDispositivo { get; set; }
        public string Cidade { get; set; }
        public string TemperaturaMinima { get; set; }
        public string VentoMinimo { get; set; }
        public bool Chuva { get; set; }
    }
}
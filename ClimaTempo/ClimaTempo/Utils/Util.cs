using System;
using System.Threading.Tasks;

namespace ClimaTempo.Utils
{
    public class Util
    {
        public static readonly string BattutaKey = "00000000000000000000000000000000";
        public static readonly string UrlBaseBattuta = "https://geo-battuta.net/api";

        public static readonly string OpenWeatherKey = "a53e5ba63332330313ea1e544155483b";
        public static readonly string UrlBaseOpenWeather = "http://api.openweathermap.org/data/2.5";

        public static readonly string UrlBaseFirebase = "https://climatempo-16094.firebaseio.com";

        public async Task ObterBattutaKey()
        {
            try
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("battuta_key", "secret-oauth-token-value");
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }

        public async Task ObterOpenWeatherKey()
        {
            try
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("open_Weather_key", "secret-oauth-token-value");
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }
    }
}
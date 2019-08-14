using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ClimaTempo.Models.Battuta;
using ClimaTempo.Models.OpenWeather;
using ClimaTempo.Services.Interfaces;
using Prism.Navigation;

namespace ClimaTempo.ViewModels
{
    public class ConfiguracoesPageViewModel : ViewModelBase
    {
        private readonly IOpenWeatherService _openWeatherService;
        public ConfiguracoesPageViewModel(INavigationService navigationService,
            IOpenWeatherService openWeatherService) : base(navigationService)
        {
            _openWeatherService = openWeatherService;

            ClimaAtual = new ClimaAtual();

            Title = "Configurações";
        }

        #region Observables

        public ClimaAtual ClimaAtual { get; set; }

        #endregion

        private async Task ObterClimaTempo(string cidade)
        {
            ClimaAtual = await _openWeatherService.ObterClimaTempo(cidade);
            RaisePropertyChanged(nameof(ClimaAtual));
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await ObterClimaTempo(parameters.GetValue<string>("Cidade"));
        }
    }
}
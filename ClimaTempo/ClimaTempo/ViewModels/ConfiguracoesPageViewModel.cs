using ClimaTempo.Models.OpenWeather;
using ClimaTempo.Services.Interfaces;
using Prism.Navigation;
using System.Threading.Tasks;
using Prism.Commands;
using Xamarin.Forms.Internals;

namespace ClimaTempo.ViewModels
{
    public class ConfiguracoesPageViewModel : ViewModelBase
    {
        private readonly IOpenWeatherService _openWeatherService;

        #region Propriedades

        private bool _temperaturaMinina;
        private bool _ventoMinimo;
        private bool _chuva;

        public bool TemperaturaMinina
        {
            get => _temperaturaMinina;
            set => SetProperty(ref _temperaturaMinina, value);
        }

        public bool VentoMinimo
        {
            get => _ventoMinimo;
            set => SetProperty(ref _ventoMinimo, value);
        }

        public bool Chuva
        {
            get => _chuva;
            set => SetProperty(ref _chuva, value);
        }

        #endregion

        public ConfiguracoesPageViewModel(INavigationService navigationService,
            IOpenWeatherService openWeatherService) : base(navigationService)
        {
            _openWeatherService = openWeatherService;

            ClimaAtual = new ClimaAtual();

            Title = "Configurações";

            //SalvarConfiguracoesCommand = new DelegateCommand(async () => await SalvarDados());
        }

        #region Commands

        public DelegateCommand SalvarConfiguracoesCommand { get; set; }

        #endregion

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

        public async void SalvarDados()
        {
            var dispositovo = ObterNomeDispositivo();
            var cidade = "";
            var temperaturaMinima = "";
            var ventoMinimo = "";
            var chuva = true;
        }

        public string ObterNomeDispositivo()
        {
            var dispositivoSerie = typeof(DeviceInfo).GUID.ToString();

            return null;
        }
    }
}
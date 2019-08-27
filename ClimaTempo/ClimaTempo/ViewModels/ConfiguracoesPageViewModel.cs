using ClimaTempo.Models;
using ClimaTempo.Models.OpenWeather;
using ClimaTempo.Services.Interfaces;
using Microsoft.AppCenter;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace ClimaTempo.ViewModels
{
    public class ConfiguracoesPageViewModel : ViewModelBase
    {
        private readonly IOpenWeatherService _openWeatherService;
        private readonly IFirebaseService _firebaseService;
        private readonly IPageDialogService _pageDialogService;

        public ConfiguracoesPageViewModel(INavigationService navigationService,
            IOpenWeatherService openWeatherService, 
            IFirebaseService firebaseService, 
            IPageDialogService pageDialogService) : base(navigationService)
        {
            _openWeatherService = openWeatherService;
            _firebaseService = firebaseService;
            _pageDialogService = pageDialogService;

            ClimaAtual = new ClimaAtual();

            Title = "Configurações";

            SalvarConfiguracoesCommand = new DelegateCommand(async () => await SalvarConfiguracoes(), PodeExecutarSalvarConfiguracoesCommand);
        }

        #region Propriedades
       
        private bool _temperaturaMinima;
        public bool TemperaturaMinima
        {
            get => _temperaturaMinima;
            set => SetProperty(ref _temperaturaMinima, value);
        }

        private bool _ventoMinimo;
        public bool VentoMinimo
        {
            get => _ventoMinimo;
            set => SetProperty(ref _ventoMinimo, value);
        }

        private bool _chuva;
        public bool Chuva
        {
            get => _chuva;
            set
            {
                SetProperty(ref _chuva, value);
                SalvarConfiguracoesCommand.RaiseCanExecuteChanged();
            }
        }

        private double _valorTemperaturaMinima;
        public double ValorTemperaturaMinima
        {
            get => _valorTemperaturaMinima;
            set
            {
                SetProperty(ref _valorTemperaturaMinima, value);
                SalvarConfiguracoesCommand.RaiseCanExecuteChanged();
            }
        }

        private double _valorVentoMinimo;
        public double ValorVentoMinimo
        {
            get => _valorVentoMinimo;
            set
            {
                SetProperty(ref _valorVentoMinimo, value);
                SalvarConfiguracoesCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Commands

        public DelegateCommand SalvarConfiguracoesCommand { get; set; }

        #endregion

        #region Observables

        public ClimaAtual ClimaAtual { get; set; }

        #endregion

        #region Métodos de Comando

        bool PodeExecutarSalvarConfiguracoesCommand()
        {
            return ValorTemperaturaMinima > 0 || Chuva || ValorVentoMinimo > 0;
        }

        private async Task ObterClimaTempo(string cidade)
        {
            ClimaAtual = await _openWeatherService.ObterClimaTempo(cidade);
            RaisePropertyChanged(nameof(ClimaAtual));
        }

        private async Task SalvarConfiguracoes()
        {
            var notificacao = new Notificacao
            {
                IdDispositivo = AppCenter.GetInstallIdAsync().Result.GetValueOrDefault(),
                Chuva = Chuva,
                Cidade = ClimaAtual.Nome,
                TemperaturaMinima = ValorTemperaturaMinima,
                VentoMinimo = ValorVentoMinimo
            };

            var resultado = await _firebaseService.AdicionarNotificacao(notificacao);

            if (resultado != null)
                await _pageDialogService.DisplayAlertAsync("Sucesso", "Configuração salva com sucesso", "OK");
        }

        #endregion

        #region Overrides

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await ObterClimaTempo(parameters.GetValue<string>("Cidade"));
        }

        #endregion
    }
}
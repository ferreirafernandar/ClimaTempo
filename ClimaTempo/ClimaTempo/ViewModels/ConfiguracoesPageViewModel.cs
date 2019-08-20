using ClimaTempo.Models;
using ClimaTempo.Models.OpenWeather;
using ClimaTempo.Services.Interfaces;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace ClimaTempo.ViewModels
{
    public class ConfiguracoesPageViewModel : ViewModelBase
    {
        private readonly IOpenWeatherService _openWeatherService;
        private readonly IFirebaseService _firebaseService;

        public ConfiguracoesPageViewModel(INavigationService navigationService,
            IOpenWeatherService openWeatherService, IFirebaseService firebaseService) : base(navigationService)
        {
            _openWeatherService = openWeatherService;
            _firebaseService = firebaseService;

            ClimaAtual = new ClimaAtual();
            Notificacao = new Notificacao();

            Title = "Configurações";

            SalvarConfiguracoesCommand = new DelegateCommand(async () => await SalvarConfiguracoes());
        }

        #region Propriedades

        private bool _temperaturaMinina;
        private bool _ventoMinimo;
        private bool _chuva;
        private Notificacao _notificacaoSelecionada;

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

        public Notificacao NotificacaoSelecionada
        {
            get => _notificacaoSelecionada;
            set => SetProperty(ref _notificacaoSelecionada, value);
        }

        #endregion

        #region Commands

        public DelegateCommand SalvarConfiguracoesCommand { get; set; }

        #endregion

        #region Observables

        public ClimaAtual ClimaAtual { get; set; }
        public Notificacao Notificacao { get; set; }

        #endregion

        private async Task ObterClimaTempo(string cidade)
        {
            ClimaAtual = await _openWeatherService.ObterClimaTempo(cidade);
            RaisePropertyChanged(nameof(ClimaAtual));
        }

        private async Task SalvarConfiguracoes()
        {
            await _firebaseService.AdicionarNotificacao(NotificacaoSelecionada);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await ObterClimaTempo(parameters.GetValue<string>("Cidade"));
            _openWeatherService.GerarIdDispositivo();
        }
    }
}
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ClimaTempo.Models.Battuta;
using ClimaTempo.Services.Interfaces;
using Prism.Commands;
using Prism.Navigation;

namespace ClimaTempo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IBattutaService _battutaService;
        protected readonly INavigationService _navigationService;
        public MainPageViewModel(INavigationService navigationService, IBattutaService battutaService) : base(navigationService)
        {
            _navigationService = navigationService;
            _battutaService = battutaService;
            Title = "Busca";

            Paises = new ObservableCollection<Pais>();
            Estados = new ObservableCollection<Estado>();
            Cidades = new ObservableCollection<Cidade>();

            AtualizarListaEstadosCommand = new DelegateCommand(async () => await PreencherEstados());
            AtualizarListaCidadesCommand = new DelegateCommand(async () => await PreencherCidades());
            IrParaConfiguracoesCommand = new DelegateCommand(async () => await IrParaConfiguracoes());
        }

        #region Observables

        public ObservableCollection<Pais> Paises { get; set; }
        public ObservableCollection<Estado> Estados { get; set; }
        public ObservableCollection<Cidade> Cidades { get; set; }

        #endregion

        #region Commands

        public DelegateCommand AtualizarListaEstadosCommand { get; set; }
        public DelegateCommand AtualizarListaCidadesCommand { get; set; }
        public DelegateCommand IrParaConfiguracoesCommand { get; set; }

        #endregion

        #region Métodos Comandos 

        private async Task PreencherPaises()
        {
            var paises = await _battutaService.ObterPaises();

            Paises.Clear();

            foreach (var pais in paises)
                Paises.Add(pais);
        }

        private async Task PreencherEstados()
        {
            var estados = await _battutaService.ObterEstados(PaisSelecionado.Codigo);

            Estados.Clear();

            foreach (var estado in estados)
                Estados.Add(estado);
        }

        private async Task PreencherCidades()
        {
            var cidades = await _battutaService.ObterCidades(PaisSelecionado.Codigo, EstadoSelecionado.NomeEstado);

            Cidades.Clear();

            foreach (var cidade in cidades)
                Cidades.Add(cidade);
        }

        private async Task IrParaConfiguracoes()
        {
            var navigationParameters = new NavigationParameters
            {
                {
                    "Cidade", CidadeSelecionada.NomeCidade
                }
            };

            await _navigationService.NavigateAsync("ConfiguracoesPage", navigationParameters);
        }

        #endregion

        #region Propriedades

        private Pais _paisSelecionado;
        public Pais PaisSelecionado
        {
            get => _paisSelecionado;
            set => SetProperty(ref _paisSelecionado, value);
        }

        private Estado _estadoSelecionado;
        public Estado EstadoSelecionado
        {
            get => _estadoSelecionado;
            set => SetProperty(ref _estadoSelecionado, value);
        }

        private Cidade _cidadeSelecionada;
        public Cidade CidadeSelecionada
        {
            get => _cidadeSelecionada;
            set => SetProperty(ref _cidadeSelecionada, value);
        }

        #endregion

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await PreencherPaises();
        }
    }
}

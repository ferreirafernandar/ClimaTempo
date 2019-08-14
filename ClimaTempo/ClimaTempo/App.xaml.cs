using ClimaTempo.Services;
using ClimaTempo.Services.Interfaces;
using Prism;
using Prism.Ioc;
using ClimaTempo.ViewModels;
using ClimaTempo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ClimaTempo
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //Enable HotReloader
#if DEBUG
            HotReloader.Current.Run(this);    
#endif
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ConfiguracoesPage, ConfiguracoesPageViewModel>();
            containerRegistry.Register<IBattutaService, BattutaService>();
            containerRegistry.Register<IOpenWeatherService, OpenWeatherService>();
            containerRegistry.Register<IHttpClient, HttpClient>();
        }
    }
}

using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Prism;
using Prism.Ioc;

namespace ClimaTempo.Droid
{
    [Activity(Label = "ClimaTempo", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            AppCenter.Start("60bc56f7-315b-43b5-94fc-bad53e105013", typeof(Push));
            LoadApplication(new App(new AndroidInitializer()));

            AppCenter.GetInstallIdAsync().ContinueWith(installId =>
            {
                System.Diagnostics.Debug.WriteLine($"VS App Center InstallId={installId.Result}");
            });
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}


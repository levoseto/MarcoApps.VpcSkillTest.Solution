using MarcoApps.VpcSkillTest.Services.Mobile.Services;
using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;
using MarcoApps.VpcSkillTest.Services.Mobile.Views;
using Microsoft.Extensions.Logging;

namespace MarcoApps.VpcSkillTest.Services.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Registrar HttpClient y HttpService
            builder.Services.AddHttpClient<HttpService>(client =>
            {
                client.BaseAddress = new Uri("https://5cjpkcs3-7172.usw3.devtunnels.ms/api/");
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            // Registrar ViewModels
            builder.Services.AddSingleton<SolicitudesViewModel>();
            builder.Services.AddTransient<SolicitudPiezaViewModel>();

            // Registrar Views
            builder.Services.AddSingleton<SolicitudesPage>();
            builder.Services.AddTransient<SolicitudPiezaPage>();

            return builder.Build();
        }
    }
}
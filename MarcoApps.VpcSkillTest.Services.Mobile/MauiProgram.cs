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
            // DevTunnel PC: 3x8xgrg6
            // DevTunnel Laptop SSA: 5cjpkcs3
            // DevTunner Laptop Huawei:
            string prefixUrl = "5cjpkcs3";

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
            builder.Services.AddSingleton<App>();

            builder.Services.AddSingleton<AuthService>();

            // Registrar HttpClient y HttpService
            builder.Services.AddHttpClient<HttpService>(client =>
            {
                client.BaseAddress = new Uri($"https://{prefixUrl}-7172.usw3.devtunnels.ms/api/");
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            // Registrar ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<SolicitudesViewModel>();
            builder.Services.AddTransient<SolicitudPiezaViewModel>();
            builder.Services.AddTransient<EditarSolicitudViewModel>();
            builder.Services.AddTransient<SolicitudProveedorViewModel>();
            builder.Services.AddTransient<InstalacionViewModel>();

            // Registrar Views
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<SolicitudesPage>();
            builder.Services.AddTransient<SolicitudPiezaPage>();
            builder.Services.AddTransient<EditarSolicitudPiezaPage>();
            builder.Services.AddTransient<SolicitudProveedorPage>();
            builder.Services.AddTransient<InstalacionPage>();

            return builder.Build();
        }
    }
}
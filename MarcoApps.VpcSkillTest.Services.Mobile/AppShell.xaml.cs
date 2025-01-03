using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;
using MarcoApps.VpcSkillTest.Services.Mobile.Views;

namespace MarcoApps.VpcSkillTest.Services.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly MainViewModel mainViewModel;

        public AppShell(MainViewModel mainViewModel)
        {
            InitializeComponent();
            RegisterRoutes();
            this.mainViewModel = mainViewModel;
            BindingContext = mainViewModel;
        }

        private void RegisterRoutes()
        {
            // Registrar la ruta para la página que no estará en el menú
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("solicitudes/nueva", typeof(SolicitudPiezaPage));
            Routing.RegisterRoute("solicitudes/edita", typeof(EditarSolicitudPiezaPage));
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);

            // Verificar si la página actual es el Login
            var currentRoute = Shell.Current.CurrentState.Location.OriginalString;

            if (currentRoute.Contains("login"))
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled; // Deshabilitar el menú
            }
            else
            {
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout; // Restaurar el menú
            }
        }
    }
}
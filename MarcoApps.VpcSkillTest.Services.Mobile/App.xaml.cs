using MarcoApps.VpcSkillTest.Services.Mobile.Services;
using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;

namespace MarcoApps.VpcSkillTest.Services.Mobile
{
    public partial class App : Application
    {
        private readonly MainViewModel _mainViewModel;
        public static AuthService _authService;

        public static DatabaseService Database { get; private set; }
        private readonly string? _DbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public App(MainViewModel mainViewModel,
            AuthService authService)
        {
            InitializeComponent();
            this._mainViewModel = mainViewModel;
            _authService = authService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Database = new DatabaseService(_DbPath!);
            return new Window(new AppShell(_mainViewModel));
        }

        protected override async void OnStart()
        {
            // Acceder al servicio AuthService desde el contenedor de servicios
            var authService = Current.Handler.MauiContext.Services.GetService<AuthService>();
            await authService.LoadUserAsync();
            if (authService != null && authService.IsAuthenticated)
            {
                // Si el usuario está autenticado, redirigir a la pantalla principal
                Shell.Current.GoToAsync("main");
            }
            else
            {
                // De lo contrario, redirigir a la pantalla de inicio de sesión
                Shell.Current.GoToAsync("login");
            }
        }
    }
}
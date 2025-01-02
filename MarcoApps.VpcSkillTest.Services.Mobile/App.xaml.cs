using MarcoApps.VpcSkillTest.Services.Mobile.Services;

namespace MarcoApps.VpcSkillTest.Services.Mobile
{
    public partial class App : Application
    {
        public static AuthService _authService;

        public static DatabaseService Database { get; private set; }
        private readonly string? _DbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public App(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Database = new DatabaseService(_DbPath!);
            return new Window(new AppShell());
        }

        protected override void OnStart()
        {
            // Acceder al servicio AuthService desde el contenedor de servicios
            var authService = Current.Handler.MauiContext.Services.GetService<AuthService>();

            if (authService != null && authService.IsAuthenticated)
            {
                // Si el usuario está autenticado, redirigir a la pantalla principal
                Shell.Current.GoToAsync("//main");
            }
            else
            {
                // De lo contrario, redirigir a la pantalla de inicio de sesión
                Shell.Current.GoToAsync("//login");
            }
        }
    }
}
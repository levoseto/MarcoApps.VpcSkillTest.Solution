using MarcoApps.VpcSkillTest.Services.Mobile.Views;

namespace MarcoApps.VpcSkillTest.Services.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            // Registrar la ruta para la página que no estará en el menú
            Routing.RegisterRoute("solicitudes/nueva", typeof(SolicitudPiezaPage));
        }
    }
}
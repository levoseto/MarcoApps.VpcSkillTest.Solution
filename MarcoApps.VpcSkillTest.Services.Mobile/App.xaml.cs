using MarcoApps.VpcSkillTest.Services.Mobile.Services;

namespace MarcoApps.VpcSkillTest.Services.Mobile
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }
        private readonly string? _DbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Database = new DatabaseService(_DbPath!);
            return new Window(new AppShell());
        }
    }
}
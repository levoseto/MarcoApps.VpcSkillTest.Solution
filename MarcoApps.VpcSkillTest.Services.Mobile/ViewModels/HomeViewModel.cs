using MarcoApps.VpcSkillTest.Services.Mobile.Models;
using MarcoApps.VpcSkillTest.Services.Mobile.Services;
using System.Windows.Input;

namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly HttpService _httpService;

        private string _mecanicoNombre;

        public string MecanicoNombre
        {
            get => _mecanicoNombre;
            set => SetProperty(ref _mecanicoNombre, value);
        }

        private string _tallerNombre;

        public string TallerNombre
        {
            get => _tallerNombre;
            set => SetProperty(ref _tallerNombre, value);
        }

        private AuthUser _currentUser;

        public AuthUser CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public int TotalSolicitudesRealizadas { get; set; }
        public int TotalSolicitudesRecibidas { get; set; }
        public int TotalInstalaciones { get; set; }

        public ICommand NavigateToSolicitudesRealizadasCommand { get; }
        public ICommand NavigateToSolicitudesRecibidasCommand { get; }
        public ICommand NavigateToInstalacionCommand { get; }

        public HomeViewModel(
              AuthService authService
            , HttpService httpService
            )
        {
            _authService = authService;
            _httpService = httpService;
            CurrentUser = _authService.CurrentUser;
            NavigateToSolicitudesRealizadasCommand = new Command(async () => await Shell.Current.GoToAsync("///solicitudes"));
            NavigateToSolicitudesRecibidasCommand = new Command(async () => await Shell.Current.GoToAsync("///recibidas"));
            NavigateToInstalacionCommand = new Command(async () => await Shell.Current.GoToAsync("///instalacion"));
            MecanicoNombre = "";
            TallerNombre = "";
        }

        public void LoadData()
        {
            CurrentUser = _authService.CurrentUser;
            MecanicoNombre = (CurrentUser is null || string.IsNullOrEmpty(CurrentUser.Nombre)) ? "Invitado" : CurrentUser.Nombre;
            TallerNombre = (CurrentUser is null || string.IsNullOrEmpty(CurrentUser.TallerNombre)) ? "Sin taller" : CurrentUser.TallerNombre;
            // Simulación de datos. Integra servicios reales para datos en vivo.
            TotalSolicitudesRealizadas = 15;
            TotalSolicitudesRecibidas = 8;
            TotalInstalaciones = 23;

            OnPropertyChanged(nameof(TotalSolicitudesRealizadas));
            OnPropertyChanged(nameof(TotalSolicitudesRecibidas));
            OnPropertyChanged(nameof(TotalInstalaciones));
        }
    }
}
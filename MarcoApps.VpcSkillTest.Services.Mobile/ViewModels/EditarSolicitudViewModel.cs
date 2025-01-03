using MarcoApps.VpcSkillTest.Services.Mobile.Models;
using MarcoApps.VpcSkillTest.Services.Mobile.Models.DTO;
using MarcoApps.VpcSkillTest.Services.Mobile.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    public class EditarSolicitudViewModel : BaseViewModel
    {
        private readonly HttpService _httpService;

        private ObservableCollection<Taller> _talleres;

        public ObservableCollection<Taller> Talleres
        {
            get => _talleres;
            set => SetProperty(ref _talleres, value);
        }

        private ObservableCollection<Refaccion> _refacciones;

        public ObservableCollection<Refaccion> Refacciones
        {
            get => _refacciones;
            set => SetProperty(ref _refacciones, value);
        }

        private ObservableCollection<Vehiculo> _Vehiculos;

        public ObservableCollection<Vehiculo> Vehiculos
        {
            get => _Vehiculos;
            set => SetProperty(ref _Vehiculos, value);
        }

        private Taller _selectedTaller;

        public Taller SelectedTaller
        {
            get => _selectedTaller;
            set => SetProperty(ref _selectedTaller, value);
        }

        private Refaccion _selectedRefaccion;

        public Refaccion SelectedRefaccion
        {
            get => _selectedRefaccion;
            set => SetProperty(ref _selectedRefaccion, value);
        }

        private Vehiculo _selectedVehiculo;

        public Vehiculo SelectedVehiculo
        {
            get => _selectedVehiculo;
            set => SetProperty(ref _selectedVehiculo, value);
        }

        private int _SolicitudId;

        public int SolicitudId
        {
            get => _SolicitudId;
            set => SetProperty(ref _SolicitudId, value);
        }

        private string _estado;

        public string Estado
        {
            get => _estado;
            set => SetProperty(ref _estado, value);
        }

        private double _LatitudSolicita;

        public double LatitudSolicita
        {
            get => _LatitudSolicita;
            set => SetProperty(ref _LatitudSolicita, value);
        }

        private double _LonitudSolicita;

        public double LLonitudSolicita
        {
            get => _LonitudSolicita;
            set => SetProperty(ref _LonitudSolicita, value);
        }

        private DateTime _FechaSolicitud;

        public DateTime FechaSolicitud
        {
            get => _FechaSolicitud;
            set => SetProperty(ref _FechaSolicitud, value);
        }

        public ICommand GuardarCambiosCommand { get; }

        public EditarSolicitudViewModel(HttpService httpService)
        {
            _httpService = httpService;
            Talleres = new ObservableCollection<Taller>();
            Refacciones = new ObservableCollection<Refaccion>();
            Vehiculos = new ObservableCollection<Vehiculo>();

            GuardarCambiosCommand = new Command(async () => await GuardarCambiosAsync());
        }

        public async Task Inicializar(SolicitudConsultaDto solicitudConsultaDto)
        {
            var solicitud = await App.Database.GetConnection().Table<Solicitud>().FirstOrDefaultAsync(s => s.SolicitudId == solicitudConsultaDto.SolicitudId);
            if (solicitud is not null)
            {
                SolicitudId = solicitudConsultaDto.SolicitudId;
                SelectedTaller = Talleres.FirstOrDefault(t => t.TallerId == solicitud.TallerProveedorId)!;
                SelectedRefaccion = Refacciones.FirstOrDefault(r => r.RefaccionId == solicitud.RefaccionId)!;
                SelectedVehiculo = Vehiculos.FirstOrDefault(v => v.VehiculoId == solicitud.VehiculoId)!;
                Estado = solicitud.Estado;
                FechaSolicitud = solicitudConsultaDto.FechaSolicitud;
            }
        }

        public async Task LoadDataAsync()
        {
            try
            {
                SelectedVehiculo = null;
                SelectedRefaccion = null;
                SelectedTaller = null;
                await CargarVehiculosAsync();
                await CargarCatalogoTalleresAsync();
                await CargarCatalogoRefaccionesAsync();
            }
            catch (Exception ex)
            {
                // Si falla la conexión, carga datos locales
                Talleres = new(await App.Database.GetConnection().Table<Taller>().ToListAsync());
                Refacciones = new(await App.Database.GetConnection().Table<Refaccion>().ToListAsync());
                Vehiculos = new(await App.Database.GetConnection().Table<Vehiculo>().ToListAsync());

                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar datos: {ex.Message}", "OK");
            }
        }

        public async Task CargarVehiculosAsync()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    // Cargar vehículos desde la API
                    var vehiculosApi = await _httpService.GetAsync<List<Vehiculo>>("vehiculo");
                    SetCollection(ref _Vehiculos, vehiculosApi); // Usa SetCollection aquí
                    await App.Database.GetConnection().DeleteAllAsync<Vehiculo>();
                    await App.Database.GetConnection().InsertAllAsync(vehiculosApi);
                }
            }
            catch (Exception ex)
            {
                var vehiculosLocal = await App.Database.GetConnection().Table<Vehiculo>().ToListAsync();
                SetCollection(ref _Vehiculos, vehiculosLocal); // También usa SetCollection aquí
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar talleres: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task CargarCatalogoTalleresAsync()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var talleresApi = await _httpService.GetAsync<List<Taller>>("taller");
                    SetCollection(ref _talleres, talleresApi); // Usa SetCollection aquí
                    await App.Database.GetConnection().DeleteAllAsync<Taller>();
                    await App.Database.GetConnection().InsertAllAsync(talleresApi);
                }
            }
            catch (Exception ex)
            {
                var talleresLocal = await App.Database.GetConnection().Table<Taller>().ToListAsync();
                SetCollection(ref _talleres, talleresLocal); // También usa SetCollection aquí
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar talleres: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task CargarCatalogoRefaccionesAsync()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var refaccionesApi = await _httpService.GetAsync<List<Refaccion>>("refaccion");
                    SetCollection(ref _refacciones, refaccionesApi); // Usa SetCollection aquí
                    await App.Database.GetConnection().DeleteAllAsync<Refaccion>();
                    await App.Database.GetConnection().InsertAllAsync(refaccionesApi);
                }
            }
            catch (Exception ex)
            {
                var refaccionesLocal = await App.Database.GetConnection().Table<Refaccion>().ToListAsync();
                SetCollection(ref _refacciones, refaccionesLocal); // También usa SetCollection aquí
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar refacciones: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GuardarCambiosAsync()
        {
            IsBusy = false;

            if (SelectedTaller == null || SelectedRefaccion == null || SelectedVehiculo == null)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Seleccione un taller y una refacción para asignar a un vehículo.", "OK");
                return;
            }

            var solicitudDto = new Solicitud
            {
                SolicitudId = SolicitudId, // ID de ejemplo
                TallerProveedorId = SelectedTaller.TallerId,
                TallerSolicitanteId = App._authService.CurrentUser.TallerId,
                RefaccionId = SelectedRefaccion.RefaccionId,
                VehiculoId = SelectedVehiculo.VehiculoId,
                FechaSolicitud = FechaSolicitud,
                Estado = Estado,
                MecanicoSolicitanteId = App._authService.CurrentUser.MecanicoId,
            };

            try
            {
                var response = await _httpService.PutAsync<Solicitud>($"solicitud/editar/{solicitudDto.SolicitudId}", solicitudDto);

                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Solicitud actualizada correctamente.", "OK");
                    await Shell.Current.GoToAsync(".."); // Regresar a la pantalla anterior
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error al actualizar: {response.ReasonPhrase}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
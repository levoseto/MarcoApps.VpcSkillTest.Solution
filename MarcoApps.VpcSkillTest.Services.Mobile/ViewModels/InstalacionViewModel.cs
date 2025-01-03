using MarcoApps.VpcSkillTest.Services.Mobile.Models;
using MarcoApps.VpcSkillTest.Services.Mobile.Models.DTO;
using MarcoApps.VpcSkillTest.Services.Mobile.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    public class InstalacionViewModel : BaseViewModel
    {
        private readonly HttpService _httpService;

        #region Attributes

        private ObservableCollection<Vehiculo> _Vehiculos;
        private ObservableCollection<Refaccion> _Refacciones;

        private Vehiculo _selectedVehiculo;
        private Refaccion _selectedRefaccion;

        private string _estadoMensaje;
        private bool _mostrarEstado;
        private bool _canRegistrar;

        #endregion Attributes

        #region Properties

        public ObservableCollection<Vehiculo> Vehiculos
        {
            get => _Vehiculos;
            set => SetProperty(ref _Vehiculos, value);
        }

        public ObservableCollection<Refaccion> Refacciones
        {
            get => _Refacciones;
            set => SetProperty(ref _Refacciones, value);
        }

        public Vehiculo SelectedVehiculo
        {
            get => _selectedVehiculo;
            set
            {
                SetProperty(ref _selectedVehiculo, value);
                ValidateInputs();
            }
        }

        public Refaccion SelectedRefaccion
        {
            get => _selectedRefaccion;
            set
            {
                SetProperty(ref _selectedRefaccion, value);
                ValidateInputs();
            }
        }

        public string EstadoMensaje
        {
            get => _estadoMensaje;
            set => SetProperty(ref _estadoMensaje, value);
        }

        public bool MostrarEstado
        {
            get => _mostrarEstado;
            set => SetProperty(ref _mostrarEstado, value);
        }

        public bool CanRegistrar
        {
            get => _canRegistrar;
            set => SetProperty(ref _canRegistrar, value);
        }

        #endregion Properties

        public ICommand RegistrarInstalacionCommand { get; }

        public InstalacionViewModel(HttpService httpService)
        {
            _httpService = httpService;
            Vehiculos = new ObservableCollection<Vehiculo>();
            Refacciones = new ObservableCollection<Refaccion>();
            RegistrarInstalacionCommand = new Command(async () => await RegistrarInstalacionAsync());
        }

        public async Task CargarDatosAsync()
        {
            IsBusy = true;

            try
            {
                await CargarVehiculosAsync();
                await CargarCatalogoRefaccionesAsync();
            }
            catch (Exception ex)
            {
                EstadoMensaje = $"Error al cargar datos: {ex.Message}";
                MostrarEstado = true;
            }
            finally
            {
                IsBusy = false;
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

        public async Task CargarCatalogoRefaccionesAsync()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var refaccionesApi = await _httpService.GetAsync<List<Refaccion>>("refaccion");
                    SetCollection(ref _Refacciones, refaccionesApi); // Usa SetCollection aquí
                    await App.Database.GetConnection().DeleteAllAsync<Refaccion>();
                    await App.Database.GetConnection().InsertAllAsync(refaccionesApi);
                }
            }
            catch (Exception ex)
            {
                var refaccionesLocal = await App.Database.GetConnection().Table<Refaccion>().ToListAsync();
                SetCollection(ref _Refacciones, refaccionesLocal); // También usa SetCollection aquí
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar refacciones: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RegistrarInstalacionAsync()
        {
            IsBusy = true;
            MostrarEstado = false;

            try
            {
                var instalacionDto = new InstalacionDto()
                {
                    VehiculoId = SelectedVehiculo.VehiculoId,
                    RefaccionId = SelectedRefaccion.RefaccionId,
                    TallerId = 1, // ID del taller actual (configurable)
                    MecanicoId = 1, // ID del mecánico actual (configurable)
                    Latitud = 19.432608,
                    Longitud = -99.133209
                };

                var response = await _httpService.PostAsync("instalacion/registra", instalacionDto);

                if (response.IsSuccessStatusCode)
                {
                    EstadoMensaje = "Instalación registrada correctamente.";
                }
                else
                {
                    EstadoMensaje = $"Error al registrar instalación: {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                EstadoMensaje = $"Error: {ex.Message}";
            }
            finally
            {
                MostrarEstado = true;
                IsBusy = false;
            }
        }

        private void ValidateInputs()
        {
            CanRegistrar = SelectedVehiculo != null && SelectedRefaccion != null;
        }
    }
}
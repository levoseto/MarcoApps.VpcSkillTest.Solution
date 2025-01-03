namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    using MarcoApps.VpcSkillTest.Services.Mobile.Models;
    using MarcoApps.VpcSkillTest.Services.Mobile.Services;
    using MarcoApps.VpcSkillTest.Services.Mobile.Tools.Extensions;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class SolicitudPiezaViewModel : BaseViewModel
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

        public ICommand SolicitarPiezaCommand { get; }

        public SolicitudPiezaViewModel(HttpService httpService)
        {
            _httpService = httpService;
            Talleres = new();
            Refacciones = new();
            Vehiculos = new();
            SolicitarPiezaCommand = new Command(async () => await SolicitarPiezaAsync());
        }

        public async Task LoadDataAsync()
        {
            try
            {
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
                    // Quitar el taller que es nuestro
                    var tallerActual = talleresApi.Find(t => t.TallerId == App._authService.CurrentUser.TallerId);
                    if (tallerActual is not null)
                    {
                        talleresApi.Remove(tallerActual);
                    }
                    SetCollection(ref _talleres, talleresApi);
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

        private async Task SolicitarPiezaAsync()
        {
            IsBusy = false;

            if (SelectedTaller == null || SelectedRefaccion == null || SelectedVehiculo == null)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Seleccione un taller y una refacción para asignar a un vehículo.", "OK");
                return;
            }

            try
            {
                var solicitud = new
                {
                    TallerSolicitanteId = App._authService.CurrentUser.TallerId, // ID fijo del taller solicitante
                    MecanicoSolicitanteId = App._authService.CurrentUser.MecanicoId, // ID del mecánico predeterminado
                    TallerProveedorId = SelectedTaller.TallerId,
                    SelectedRefaccion.RefaccionId,
                    SelectedVehiculo.VehiculoId,
                    FechaSolicitud = DateTime.Now,
                    Estado = "Pendiente",
                    LatitudSolicitante = 19.432608, // Latitud fija de ejemplo
                    LongitudSolicitante = -99.133209 // Longitud fija de ejemplo
                };

                var solicitudJson = solicitud.ToJson();
                System.Diagnostics.Debug.WriteLine(solicitudJson);

                var response = await _httpService.PostAsync("solicitud/registrar", solicitud);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Pieza solicitada correctamente.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error al solicitar la pieza: {response.ReasonPhrase}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al procesar solicitud: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
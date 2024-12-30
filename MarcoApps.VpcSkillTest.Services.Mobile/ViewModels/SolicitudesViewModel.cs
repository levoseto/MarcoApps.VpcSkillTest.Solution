namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    using MarcoApps.VpcSkillTest.Services.Mobile.Models;
    using System.Collections.ObjectModel;
    using System.Net.Http.Json;
    using System.Windows.Input;

    public class SolicitudesViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Solicitud> Solicitudes { get; set; }

        public ICommand LoadSolicitudesCommand { get; }
        public ICommand NuevaSolicitudCommand { get; }

        public SolicitudesViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://your-api-url.com/api/") };
            Solicitudes = new ObservableCollection<Solicitud>();
            LoadSolicitudesCommand = new Command(async () => await LoadSolicitudesAsync());
            NuevaSolicitudCommand = new Command(async () => await NuevaSolicitud());
        }

        private async Task LoadSolicitudesAsync()
        {
            IsBusy = true;
            try
            {
                // Cargar las solicitudes desde la API
                var solicitudesApi = await _httpClient.GetFromJsonAsync<List<Solicitud>>("solicitud");
                if (solicitudesApi != null)
                {
                    Solicitudes.Clear();
                    foreach (var solicitud in solicitudesApi)
                    {
                        Solicitudes.Add(solicitud);
                    }
                }

                // Sincronizar con la base de datos local
                await App.Database.GetConnection().DeleteAllAsync<Solicitud>();
                await App.Database.GetConnection().InsertAllAsync(solicitudesApi);
            }
            catch (Exception ex)
            {
                // Cargar desde la base de datos local en caso de error
                var solicitudesLocal = await App.Database.GetConnection().Table<Solicitud>().ToListAsync();
                Solicitudes = new ObservableCollection<Solicitud>(solicitudesLocal);

                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar solicitudes: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task NuevaSolicitud()
        {
            // Navegar a la pantalla para crear una nueva solicitud
            await Shell.Current.GoToAsync("//solicitudes/nueva");
        }
    }
}
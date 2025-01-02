namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    using MarcoApps.VpcSkillTest.Services.Mobile.Models;
    using MarcoApps.VpcSkillTest.Services.Mobile.Models.DTO;
    using MarcoApps.VpcSkillTest.Services.Mobile.Services;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class SolicitudesViewModel : BaseViewModel
    {
        private readonly HttpService _httpService;

        public ObservableCollection<SolicitudConsultaDto> Solicitudes { get; set; }

        public ICommand LoadSolicitudesCommand { get; }
        public ICommand NuevaSolicitudCommand { get; }
        public ICommand EliminarSolicitudCommand { get; }
        public ICommand EditarSolicitudCommand { get; }

        public SolicitudesViewModel(HttpService httpService)
        {
            _httpService = httpService;
            Solicitudes = new ObservableCollection<SolicitudConsultaDto>();
            LoadSolicitudesCommand = new Command(async () => await LoadSolicitudesAsync());
            NuevaSolicitudCommand = new Command(async () => await NuevaSolicitud());
            EliminarSolicitudCommand = new Command<int>(async (id) => await EliminarSolicitudAsync(id));
            EditarSolicitudCommand = new Command<SolicitudConsultaDto>(async (solicitudConsultaDto) => await EditarSolicitudAsync(solicitudConsultaDto));
        }

        public async Task LoadSolicitudesAsync()
        {
            IsBusy = true;
            try
            {
                // Cargar las solicitudes desde la API
                var solicitudesApi = await _httpService.GetAsync<List<SolicitudConsultaDto>>($"solicitud/taller/{App._authService.CurrentUser.TallerId}");
                if (solicitudesApi != null)
                {
                    Solicitudes.Clear();
                    foreach (var solicitud in solicitudesApi)
                    {
                        Solicitudes.Add(solicitud);
                    }
                }

                // Sincronizar con la base de datos local
                if (solicitudesApi != null && solicitudesApi.Any())
                {
                    await App.Database.GetConnection().DeleteAllAsync<Solicitud>();
                    foreach (var dto in solicitudesApi)
                    {
                        var solicitud = new Solicitud
                        {
                            SolicitudId = dto.SolicitudId,
                            TallerSolicitanteId = dto.TallerSolicitanteId,
                            TallerProveedorId = dto.TallerProveedorId,
                            RefaccionId = dto.RefaccionId,
                            MecanicoSolicitanteId = dto.MecanicoSolicitanteId,
                            VehiculoId = dto.VehiculoId,
                            Estado = dto.Estado,
                            FechaSolicitud = dto.FechaSolicitud
                        };

                        await App.Database.GetConnection().InsertAsync(solicitud);
                    }
                }
            }
            catch (Exception ex)
            {
                // Cargar desde la base de datos local en caso de error
                var solicitudesLocal = await App.Database.GetConnection().Table<SolicitudConsultaDto>().ToListAsync();
                Solicitudes = new ObservableCollection<SolicitudConsultaDto>(solicitudesLocal);

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

        private async Task EliminarSolicitudAsync(int id)
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirmación", "¿Deseas eliminar esta solicitud?", "Sí", "No");
            if (!confirm) return;

            try
            {
                // Eliminar de la Web API
                var response = await _httpService.DeleteAsync($"solicitud/{id}");
                if (response.IsSuccessStatusCode)
                {
                    // Eliminar de la base de datos local
                    var solicitud = await App.Database.GetConnection().Table<Solicitud>().FirstOrDefaultAsync(s => s.SolicitudId == id);
                    if (solicitud != null)
                    {
                        await App.Database.GetConnection().DeleteAsync(solicitud);
                    }

                    // Eliminar del ObservableCollection
                    var solicitudDto = Solicitudes.FirstOrDefault(s => s.SolicitudId == id);
                    if (solicitudDto != null)
                    {
                        Solicitudes.Remove(solicitudDto);
                    }

                    await Application.Current.MainPage.DisplayAlert("Éxito", "Solicitud eliminada correctamente.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error al eliminar la solicitud: {response.ReasonPhrase}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al eliminar la solicitud: {ex.Message}", "OK");
            }
        }

        private async Task EditarSolicitudAsync(SolicitudConsultaDto solicitudConsultaDto)
        {
            if (solicitudConsultaDto != null)
            {
                var solicitudConsultaString = System.Text.Json.JsonSerializer.Serialize(solicitudConsultaDto);
                await Shell.Current.GoToAsync($"//solicitudes/edita?solicitud={solicitudConsultaString}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al editar la solicitud: null", "OK");
            }
        }
    }
}
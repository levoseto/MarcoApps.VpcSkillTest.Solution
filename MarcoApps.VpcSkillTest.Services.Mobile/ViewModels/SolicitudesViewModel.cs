namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    using MarcoApps.VpcSkillTest.Services.Mobile.Models;
    using MarcoApps.VpcSkillTest.Services.Mobile.Models.DTO;
    using MarcoApps.VpcSkillTest.Services.Mobile.Services;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class SolicitudesViewModel : BaseViewModel
    {
        private readonly HttpService _httpService;

        public ObservableCollection<SolicitudConsultaDto> Solicitudes { get; set; }

        public ICommand LoadSolicitudesCommand { get; }
        public ICommand NuevaSolicitudCommand { get; }

        public SolicitudesViewModel(HttpService httpService)
        {
            _httpService = httpService;
            Solicitudes = new ObservableCollection<SolicitudConsultaDto>();
            LoadSolicitudesCommand = new Command(async () => await LoadSolicitudesAsync());
            NuevaSolicitudCommand = new Command(async () => await NuevaSolicitud());
        }

        public async Task LoadSolicitudesAsync()
        {
            IsBusy = true;
            try
            {
                // Cargar las solicitudes desde la API
                var solicitudesApi = await _httpService.GetAsync<List<SolicitudConsultaDto>>("solicitud/taller/1");
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
    }
}
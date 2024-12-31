using MarcoApps.VpcSkillTest.Services.Mobile.Models;
using MarcoApps.VpcSkillTest.Services.Mobile.Models.DTO;
using MarcoApps.VpcSkillTest.Services.Mobile.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    public class SolicitudProveedorViewModel : BaseViewModel
    {
        private readonly HttpService _httpService;

        private ObservableCollection<SolicitudConsultaDto> _solicitudes;

        public ObservableCollection<SolicitudConsultaDto> Solicitudes
        {
            get => _solicitudes;
            set => SetProperty(ref _solicitudes, value);
        }

        public ICommand LoadSolicitudesCommand { get; }
        public ICommand EnviarPiezaCommand { get; }

        public SolicitudProveedorViewModel(HttpService httpService)
        {
            _httpService = httpService;
            Solicitudes = new ObservableCollection<SolicitudConsultaDto>();
            LoadSolicitudesCommand = new Command(async () => await CargarSolicitudesAsync());
            EnviarPiezaCommand = new Command<int>(async (id) => await EnviarPiezaAsync(id));
        }

        private async Task CargarSolicitudesAsync()
        {
            IsBusy = true;

            try
            {
                int tallerId = 2; // ID del taller proveedor (puedes configurarlo dinámicamente)
                var solicitudes = await _httpService.GetAsync<List<SolicitudConsultaDto>>($"solicitud/proveedor/{tallerId}");
                if (solicitudes is not null)
                {
                    SetCollection(ref _solicitudes, solicitudes);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar solicitudes: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task EnviarPiezaAsync(int solicitudId)
        {
            var solicitud = Solicitudes.FirstOrDefault(s => s.SolicitudId == solicitudId);
            if (solicitud == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Solicitud no encontrada.", "OK");
                return;
            }

            var confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirmación",
                $"¿Deseas enviar {solicitud.Pieza} al taller solicitante?",
                "Sí", "No");

            if (!confirm) return;

            var envio = new Envio
            {
                SolicitudId = solicitud.SolicitudId,
                TallerProveedorId = solicitud.TallerProveedorId,
                TallerSolicitanteId = solicitud.TallerSolicitanteId,
                RefaccionId = solicitud.RefaccionId,
                MecanicoEnviaId = 2,
                Cantidad = 1 // Asume cantidad 1 para esta implementación
            };

            try
            {
                var response = await _httpService.PostAsync("envio/registra", envio);

                if (response.IsSuccessStatusCode)
                {
                    solicitud.Estado = "Enviada";
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Pieza enviada correctamente.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error al enviar la pieza: {response.ReasonPhrase}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error: {ex.Message}", "OK");
            }
        }
    }
}
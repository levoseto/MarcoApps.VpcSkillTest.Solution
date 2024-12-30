﻿namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    using MarcoApps.VpcSkillTest.Services.Mobile.Models;
    using System.Collections.ObjectModel;
    using System.Net.Http.Json;
    using System.Windows.Input;

    public class SolicitudPiezaViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Taller> Talleres { get; set; }
        public ObservableCollection<Refaccion> Refacciones { get; set; }

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

        public ICommand SolicitarPiezaCommand { get; }

        public SolicitudPiezaViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://your-api-url.com/api/") };
            Talleres = new ObservableCollection<Taller>();
            Refacciones = new ObservableCollection<Refaccion>();
            SolicitarPiezaCommand = new Command(async () => await SolicitarPiezaAsync());
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                // Cargar talleres desde la API y sincronizar con SQLite
                var talleresApi = await _httpClient.GetFromJsonAsync<List<Taller>>("taller");
                if (talleresApi != null)
                {
                    await App.Database.GetConnection().DeleteAllAsync<Taller>();
                    await App.Database.GetConnection().InsertAllAsync(talleresApi);
                    Talleres = new ObservableCollection<Taller>(talleresApi);
                }

                // Cargar refacciones desde la API y sincronizar con SQLite
                var refaccionesApi = await _httpClient.GetFromJsonAsync<List<Refaccion>>("refaccion");
                if (refaccionesApi != null)
                {
                    await App.Database.GetConnection().DeleteAllAsync<Refaccion>();
                    await App.Database.GetConnection().InsertAllAsync(refaccionesApi);
                    Refacciones = new ObservableCollection<Refaccion>(refaccionesApi);
                }
            }
            catch (Exception ex)
            {
                // Si falla la conexión, carga datos locales
                Talleres = new ObservableCollection<Taller>(
                    await App.Database.GetConnection().Table<Taller>().ToListAsync());
                Refacciones = new ObservableCollection<Refaccion>(
                    await App.Database.GetConnection().Table<Refaccion>().ToListAsync());

                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar datos: {ex.Message}", "OK");
            }
        }

        private async Task SolicitarPiezaAsync()
        {
            if (SelectedTaller == null || SelectedRefaccion == null)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Seleccione un taller y una refacción.", "OK");
                return;
            }

            try
            {
                var solicitud = new
                {
                    TallerSolicitanteId = 1, // ID fijo del taller solicitante
                    MecanicoSolicitanteId = 1, // ID del mecánico predeterminado
                    TallerProveedorId = SelectedTaller.TallerId,
                    RefaccionId = SelectedRefaccion.RefaccionId,
                    VehiculoId = 1, // ID fijo del vehículo (puedes adaptarlo)
                    FechaSolicitud = DateTime.Now,
                    Estado = "Pendiente",
                    LatitudSolicitante = 19.432608, // Latitud fija de ejemplo
                    LongitudSolicitante = -99.133209 // Longitud fija de ejemplo
                };

                var response = await _httpClient.PostAsJsonAsync("solicitud", solicitud);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Pieza solicitada correctamente.", "OK");
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
        }
    }
}
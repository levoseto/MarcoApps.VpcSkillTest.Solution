using MarcoApps.VpcSkillTest.Services.Mobile.Models.DTO;
using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Views;

[QueryProperty(nameof(ObjetoEdicionSolicitud), "solicitud")]
public partial class EditarSolicitudPiezaPage : ContentPage
{
    private readonly EditarSolicitudViewModel _editarSolicitudPiezaViewModel;
    private SolicitudConsultaDto _solicitudConsultaDto;

    public string ObjetoEdicionSolicitud
    {
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                var solicitudString = value; 
                _solicitudConsultaDto = System.Text.Json.JsonSerializer.Deserialize<SolicitudConsultaDto>(solicitudString);
                
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", $"Error al editar la solicitud: null", "OK");
                Shell.Current.GoToAsync("..");
            }
        }
    }

    public EditarSolicitudPiezaPage(EditarSolicitudViewModel editarSolicitudPiezaViewModel)
	{
		InitializeComponent();
        _editarSolicitudPiezaViewModel = editarSolicitudPiezaViewModel;
        BindingContext = _editarSolicitudPiezaViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _editarSolicitudPiezaViewModel.LoadDataAsync();
        await _editarSolicitudPiezaViewModel.Inicializar(_solicitudConsultaDto);
    }
}
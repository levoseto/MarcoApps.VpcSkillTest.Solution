using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Views;

public partial class SolicitudProveedorPage : ContentPage
{

    public SolicitudProveedorPage(SolicitudProveedorViewModel solicitudProveedorViewModel)
    {
        InitializeComponent();
        BindingContext = solicitudProveedorViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SolicitudProveedorViewModel viewModel)
        {
            viewModel.LoadSolicitudesCommand.Execute(null);
        }
    }
}
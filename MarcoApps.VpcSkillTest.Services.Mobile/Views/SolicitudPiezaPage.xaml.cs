using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Views;

public partial class SolicitudPiezaPage : ContentPage
{
    private readonly SolicitudPiezaViewModel viewModel;

    public SolicitudPiezaPage(SolicitudPiezaViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is null)
        {
            BindingContext = viewModel;
        }

        await viewModel.LoadData();
    }
}
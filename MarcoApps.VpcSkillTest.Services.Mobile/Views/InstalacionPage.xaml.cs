using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Views;

public partial class InstalacionPage : ContentPage
{
    public InstalacionPage(InstalacionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is InstalacionViewModel viewModel)
        {
            await viewModel.CargarDatosAsync();
        }
    }
}
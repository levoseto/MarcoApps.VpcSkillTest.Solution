using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Views;

public partial class SolicitudesPage : ContentPage
{
    public SolicitudesPage(SolicitudesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SolicitudesViewModel viewModel)
        {
            viewModel.LoadSolicitudesCommand.Execute(null);
        }
    }
}
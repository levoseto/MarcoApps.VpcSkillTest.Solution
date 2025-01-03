using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Views;

public partial class MainPage : ContentPage
{
    public MainPage(HomeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        if (BindingContext is HomeViewModel viewModel)
        {
            viewModel.LoadData();
        }
    }
}
using MarcoApps.VpcSkillTest.Services.Mobile.ViewModels;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
        UpdateImage(); // Configura la imagen inicial basada en el tema actual

        Application.Current.RequestedThemeChanged += (s, e) =>
        {
            UpdateImage(); // Actualiza la imagen cuando cambia el tema
        };
    }

    protected override void OnAppearing()
    {
        if (BindingContext is LoginViewModel loginViewModel)
        {
            loginViewModel.Initialize();
        }
    }

    private void UpdateImage()
    {
        var currentTheme = Application.Current.RequestedTheme;

        // Cambia la imagen según el tema
        LoginImage.Source = currentTheme == AppTheme.Dark
            ? "main_dark.jpg"
            : "main_light.jpg";
    }
}
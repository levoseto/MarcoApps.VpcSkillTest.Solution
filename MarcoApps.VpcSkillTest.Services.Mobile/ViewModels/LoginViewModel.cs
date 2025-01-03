namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    using MarcoApps.VpcSkillTest.Services.Mobile.Models;
    using MarcoApps.VpcSkillTest.Services.Mobile.Models.DTO;
    using MarcoApps.VpcSkillTest.Services.Mobile.Services;
    using System.Windows.Input;

    public class LoginViewModel : BaseViewModel
    {
        private readonly HttpService _httpService;
        private readonly AuthService _authService;

        public string Email { get; set; }
        public string Password { get; set; }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool _showError;

        public bool ShowError
        {
            get => _showError;
            set => SetProperty(ref _showError, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(HttpService httpService, AuthService authService)
        {
            _httpService = httpService;
            _authService = authService;
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            IsBusy = true;
            ShowError = false;

            try
            {
                var response = await _httpService.PostAsync<LoginResponseDto, LoginRequestDto>("auth/login", new LoginRequestDto
                {
                    Email = Email,
                    Password = Password
                });

                if (response == null)
                {
                    ErrorMessage = "Inicio de sesión fallido. Intente nuevamente.";
                    ShowError = true;
                    return;
                }

                var user = new AuthUser
                {
                    MecanicoId = response.MecanicoId,
                    TallerId = response.TallerId,
                    Nombre = response.Nombre,
                    Email = response.Email,
                    Telefono = response.Telefono,
                    TallerNombre = response.TallerNombre
                };

                await _authService.SaveUserAsync(user);

                // Redirigir a la pantalla principal
                await Shell.Current.GoToAsync("//main");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
                ShowError = true;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
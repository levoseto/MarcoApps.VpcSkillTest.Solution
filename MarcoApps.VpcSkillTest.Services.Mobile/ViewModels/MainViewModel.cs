using MarcoApps.VpcSkillTest.Services.Mobile.Services;
using System.Windows.Input;

namespace MarcoApps.VpcSkillTest.Services.Mobile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        public ICommand LogoutCommand { get; }

        public MainViewModel(AuthService authService)
        {
            _authService = authService;
            LogoutCommand = new Command(async () => await LogoutAsync());
        }

        private async Task LogoutAsync()
        {
            await _authService.LogoutAsync();
            await Shell.Current.GoToAsync("login");
        }
    }
}
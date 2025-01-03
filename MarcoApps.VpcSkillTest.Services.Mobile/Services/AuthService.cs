using MarcoApps.VpcSkillTest.Services.Mobile.Models;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Services
{
    public class AuthService
    {
        public AuthUser CurrentUser { get; private set; }

        public bool IsAuthenticated => CurrentUser != null;

        private const string AuthUserKey = "AuthUser";

        public void SetUser(AuthUser user)
        {
            CurrentUser = user;
        }

        public async Task LoadUserAsync()
        {
            var json = await SecureStorage.GetAsync(AuthUserKey);
            if (!string.IsNullOrEmpty(json))
            {
                CurrentUser = System.Text.Json.JsonSerializer.Deserialize<AuthUser>(json);
            }
        }

        public async Task SaveUserAsync(AuthUser user)
        {
            CurrentUser = user;
            var json = System.Text.Json.JsonSerializer.Serialize(user);
            await SecureStorage.SetAsync(AuthUserKey, json);
        }

        public async Task LogoutAsync()
        {
            CurrentUser = null;
            SecureStorage.Remove(AuthUserKey);
        }
    }
}
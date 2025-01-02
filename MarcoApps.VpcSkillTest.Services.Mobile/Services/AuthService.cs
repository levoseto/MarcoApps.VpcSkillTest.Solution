using MarcoApps.VpcSkillTest.Services.Mobile.Models;

namespace MarcoApps.VpcSkillTest.Services.Mobile.Services
{
    public class AuthService
    {
        public AuthUser CurrentUser { get; private set; }

        public bool IsAuthenticated => CurrentUser != null;

        public void SetUser(AuthUser user)
        {
            CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}


namespace Loginapp.Services
{
    public class UserSessionService
    {
        public string LoggedInEmail { get; set; } = string.Empty;

        public void Logout()
        {
            LoggedInEmail = string.Empty; // or null, your choice
        }
    }



}

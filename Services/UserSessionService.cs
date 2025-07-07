using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Loginapp.Services
{
    public class UserSessionService
    {
        private readonly ProtectedSessionStorage _sessionStorage;

        public string LoggedInEmail { get; set; } = string.Empty;

        public void Logout()
        {
            LoggedInEmail = string.Empty; // or null, your choice
        }
  

        public UserSessionService(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsOtpVerified { get; set; }

        public bool IsLoggedIn => !string.IsNullOrEmpty(FullName) && IsAuthenticated && IsOtpVerified;

        public async Task LoadSessionAsync()
        {
            var userIdResult = await _sessionStorage.GetAsync<int>("UserId");
            var fullNameResult = await _sessionStorage.GetAsync<string>("FullName");
            var roleResult = await _sessionStorage.GetAsync<string>("Role");
            var emailResult = await _sessionStorage.GetAsync<string>("Email");
            var designationResult = await _sessionStorage.GetAsync<string>("Designation");
            var isAuthenticatedResult = await _sessionStorage.GetAsync<bool>("IsAuthenticated");
            var isOtpVerifiedResult = await _sessionStorage.GetAsync<bool>("IsOtpVerified");

            if (userIdResult.Success) UserId = userIdResult.Value;
            if (fullNameResult.Success) FullName = fullNameResult.Value;
            if (roleResult.Success) Role = roleResult.Value;
            if (emailResult.Success) Email = emailResult.Value;
            if (designationResult.Success) Designation = designationResult.Value;
            if (isAuthenticatedResult.Success) IsAuthenticated = isAuthenticatedResult.Value;
            if (isOtpVerifiedResult.Success) IsOtpVerified = isOtpVerifiedResult.Value;
        }


        public async Task SetSessionAsync(int userId, string email, string fullName, string designation, string role, bool isAuthenticated, bool isOtpVerified)
        {
            UserId = userId;
            Email = email;
            FullName = fullName;
            Designation = designation;
            Role = role;
            IsAuthenticated = isAuthenticated;
            IsOtpVerified = isOtpVerified;

            await _sessionStorage.SetAsync("UserId", userId);
            await _sessionStorage.SetAsync("Email", email);
            await _sessionStorage.SetAsync("FullName", fullName);
            await _sessionStorage.SetAsync("Designation", designation);
            await _sessionStorage.SetAsync("Role", role);
            await _sessionStorage.SetAsync("IsAuthenticated", isAuthenticated);
            await _sessionStorage.SetAsync("IsOtpVerified", isOtpVerified);
        }

        public async Task ClearSessionAsync()
        {
            await _sessionStorage.DeleteAsync("UserId");
            await _sessionStorage.DeleteAsync("Email");
            await _sessionStorage.DeleteAsync("FullName");
            await _sessionStorage.DeleteAsync("Designation");
            await _sessionStorage.DeleteAsync("Role");
            await _sessionStorage.DeleteAsync("IsAuthenticated");
            await _sessionStorage.DeleteAsync("IsOtpVerified");

            UserId = 0;
            Email = null;
            FullName = null;
            Designation = null;
            Role = null;
            IsAuthenticated = false;
            IsOtpVerified = false;
        }
    }
}
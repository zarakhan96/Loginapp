using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Loginapp.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly UserSessionService _sessionService;
        private readonly Loginapp.Data.AppDbContext _dbContext;

        public CustomAuthenticationStateProvider(UserSessionService sessionService, Loginapp.Data.AppDbContext dbContext)
        {
            _sessionService = sessionService;
            _dbContext = dbContext;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = _sessionService.IsLoggedIn
                ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, _sessionService.FullName) }, "custom")
                : new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }

        public async Task VerifyOtpAsync(string otp)
        {
            var otpRecord = await _dbContext.OtpEntries
                .FirstOrDefaultAsync(o => o.Email == _sessionService.Email && o.OTP == otp && o.Expiry > DateTime.Now);
            if (otpRecord != null)
            {
                await _sessionService.SetSessionAsync(
                    _sessionService.UserId,
                    _sessionService.Email,
                    _sessionService.FullName,
                    _sessionService.Designation,
                    _sessionService.Role,
                    true,
                    true
                );
                _dbContext.OtpEntries.Remove(otpRecord);
                await _dbContext.SaveChangesAsync();
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }

        public async Task SignOutAsync()
        {
            await _sessionService.ClearSessionAsync();
            _sessionService.IsAuthenticated = false;
            _sessionService.IsOtpVerified = false;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
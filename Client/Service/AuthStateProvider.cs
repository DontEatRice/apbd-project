using System.Security.Claims;
using blazor_project.Shared.Models.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace blazor_project.Client.Service
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _service;
        private CurrentUser? _user;

        public AuthStateProvider(IAuthService service) {
            this._service = service;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try {
                var user = await GetCurrentUser();
                if (user is not null && user.IsAuthenticated) {
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, user.UserName is null ? "" : user.UserName)
                    }.Concat(user.Claims.Select(claim => new Claim(claim.Key, claim.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            } catch (HttpRequestException ex) {
                Console.WriteLine(ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task Register(Register parameters) {
            await _service.Register(parameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Login(Login parameters) {
            await _service.Login(parameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout() {
            await _service.Logout();
            _user = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<CurrentUser?> GetCurrentUser() {
            if (_user is not null && _user.IsAuthenticated)
                return _user;

            _user = await _service.CurrentUserInfo();
            return _user;
        }
    }
}
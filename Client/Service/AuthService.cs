using System.Net.Http.Json;
using blazor_project.Shared.Models.DTOs;

namespace blazor_project.Client.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<CurrentUser?> CurrentUserInfo()
        {
            return await _http.GetFromJsonAsync<CurrentUser>("api/auth/CurrentUser");
        }

        public async Task Login(Login body)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", body);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new Exception(await result.Content.ReadAsStringAsync());

            result.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            var result = await _http.PostAsync("api/auth/logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(Register body)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", body);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
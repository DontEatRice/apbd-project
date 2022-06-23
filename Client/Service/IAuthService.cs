using blazor_project.Shared.Models.DTOs;

namespace blazor_project.Client.Service
{
    public interface IAuthService
    {
        Task Login(Login body);
        Task Register(Register body);
        Task Logout();
        Task<CurrentUser?> CurrentUserInfo();
    }
}
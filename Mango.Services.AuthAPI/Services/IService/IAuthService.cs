using Mango.Services.AuthAPI.Models.DTOs;

namespace Mango.Services.AuthAPI.Services.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto request);
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task<bool> AssignRole(string email, string roleName);
    }
}

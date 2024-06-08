using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto requestDto);
        Task<ResponseDto?> LoginAsync(LoginRequestDto requestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto requestDto);
    }
}

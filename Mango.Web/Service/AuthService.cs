using Mango.Web.Models;
using Mango.Web.Models.Utility;
using Mango.Web.Service.IService;

namespace Mango.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService
        (
            IBaseService baseService
        )
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto requestDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = requestDto,
                Url = $"{SD.AuthAPIBase}/api/auth/AssignRole"
            });
        }
        /// <summary>
        ///     Logs in a user
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> LoginAsync(LoginRequestDto requestDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = requestDto,
                Url = $"{SD.AuthAPIBase}/api/auth/login"
            }, withBearer:false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto requestDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = requestDto,
                Url = $"{SD.AuthAPIBase}/api/auth/register"
            }, withBearer: false);
        }
    }
}

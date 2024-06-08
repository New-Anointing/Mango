using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models.DTOs
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = "";
    }
}

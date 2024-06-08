using Azure.Core;
using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.DTOs;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApiDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private static ApplicationUser _user = new();
        private readonly IJwtTokenGenerator _jwtTokenGenerator; 

        public AuthService
        (
            ApiDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator
        )
        {
            _context = context;
            _userManager = userManager;
            _roleManager=roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            try 
            {
                var userExist = await _userManager.FindByEmailAsync(request.UserName);
                bool isValid = await _userManager.CheckPasswordAsync(userExist, request.Password);
                if(userExist is null || isValid is false)
                {
                    return new LoginResponseDto
                    {
                        User = null,
                        Token = ""
                    };
                }
                //gen token
                var roles = await _userManager.GetRolesAsync(userExist); 
                var token = _jwtTokenGenerator.GenerateToken(userExist, roles);
                UserDto user = new()
                {
                    Email = userExist.Email,
                    ID = userExist.Id,
                    Name = userExist.Name,
                    PhoneNumber = userExist.PhoneNumber
                };

                return new LoginResponseDto
                {
                    User = user,
                    Token = token
                };
            }
            catch(Exception ex)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = null
                };
            }
        }

        public async Task<string> Register(RegistrationRequestDto request)
        {
            try
            {
                _user.Email = request.Email;
                _user.UserName = request.Email;
                _user.PhoneNumber = request.PhoneNumber;
                _user.Name = request.Name;
                var result = await _userManager.CreateAsync(_user, request.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _context.ApplicationUsers.First(u => u.UserName == request.Email);
                    var userDto = new UserDto
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                return result.Errors.FirstOrDefault().Description;


            }catch(Exception ex) 
            {
                return "Error Encountered";
            }
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var userExist = await _userManager.FindByEmailAsync(email);
            if(userExist is not null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(userExist, roleName);
                return true;
            }
            return false;
        }
    }
}

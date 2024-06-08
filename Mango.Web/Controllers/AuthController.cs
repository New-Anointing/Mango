using Mango.Web.Models;
using Mango.Web.Models.Utility;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider=tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (ModelState.IsValid)
            {
                ResponseDto result = await _authService.LoginAsync(request);
                if(result.IsSuccess && result is not null)
                {
                    LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(result.Data));
                    await SignInUser(loginResponseDto);
                    _tokenProvider.SetToken(loginResponseDto.Token);
                    TempData["Success"] = "Login Successfull";
                    return (RedirectToAction("Index", "Home"));
                }
                else
                {
                    TempData["error"] = $"Login unsuccessfull {result.Message}";
                    return View(request);
                }
            }
            return View(request);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>
            {
                new SelectListItem {Text = SD.RoleAdmin, Value= SD.RoleAdmin},
                new SelectListItem {Text = SD.RoleCustomer, Value=SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto result = await _authService.RegisterAsync(model);
                ResponseDto assignRole;
                if (result is not null && result.IsSuccess)
                {
                    if (string.IsNullOrEmpty(model.RoleName))
                    {
                        model.RoleName = SD.RoleCustomer;
                    }
                    assignRole = await _authService.AssignRoleAsync(model);
                    if(assignRole is not null && assignRole.IsSuccess) 
                    {
                        TempData["success"] = "Registration successfully";
                        return RedirectToAction(nameof(Login));

                    }

                }
                else
                {
                    var roleList = new List<SelectListItem>
                    {
                        new SelectListItem {Text = SD.RoleAdmin, Value= SD.RoleAdmin},
                        new SelectListItem {Text = SD.RoleCustomer, Value=SD.RoleCustomer}
                    };
                    ViewBag.RoleList = roleList;
                    TempData["error"] = result.Message;
                    return View(model);
                }
            }
            var roles = new List<SelectListItem>
            {
                new SelectListItem {Text = SD.RoleAdmin, Value= SD.RoleAdmin},
                new SelectListItem {Text = SD.RoleCustomer, Value=SD.RoleCustomer}
            };
            ViewBag.RoleList = roles;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return(RedirectToAction("Index", "Home"));
        }

        private async Task SignInUser(LoginResponseDto request)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(request.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}

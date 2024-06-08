using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mango.Services.AuthAPI.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {

            _jwtOptions=jwtOptions.Value;

        }
        public string GenerateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            //create a token handlery that will perform the token operations
            var tokenHandler = new JwtSecurityTokenHandler();
            //create a list of claims 
            List<Claim> claims =
            [
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                //add roles to list of claims
                //foreach (var role in roles)
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, role));
                //}
                .. roles.Select(role => new Claim(ClaimTypes.Role, role)),
            ];
            //create key and encode it
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            //create signing credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            //create a token descriptor to define the properties of the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                IssuedAt = DateTime.UtcNow,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
            };
            //use token handler to create and write the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

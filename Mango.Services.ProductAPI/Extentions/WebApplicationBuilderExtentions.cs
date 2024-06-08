using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mango.Services.ProductAPI.Extentions
{
    public static class WebApplicationBuilderExtentions
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            //add the auth service here this is what does the authourisation when a request is bein sent to the api

            //get the content of the api settingss
            var settings = builder.Configuration.GetSection("ApiSettings");

            //get each value of the settins
            var secret = settings.GetValue<string>("Secret");
            var issuer = settings.GetValue<string>("Issuer");
            var audience = settings.GetValue<string>("Audience");

            //create a key that will validate that the message is coming from a trusted sourse 
            //the private key is needed to be able compare with the issuers private key if the private keys are not the same it wont work
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                };
            });

            return builder;
        }
    }
}

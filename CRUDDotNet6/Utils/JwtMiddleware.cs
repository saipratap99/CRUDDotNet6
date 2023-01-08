using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CRUDDotNet6.Models;
using CRUDDotNet6.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CRUDDotNet6.Utils
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWT _jwtSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JWT> jwtSettings)
        {
            Console.WriteLine("JWT Middleware");
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            if (token != null)
                attachUserToContext(context, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwtSettings.ValidIssuer,
                    ValidAudience = _jwtSettings.ValidAudience,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                
                var jwtToken = (JwtSecurityToken)validatedToken;
                var email = (jwtToken.Claims.First(x => x.Type == "email").Value);
                context.Items["User"] = email;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}


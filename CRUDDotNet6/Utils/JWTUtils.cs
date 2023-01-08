using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CRUDDotNet6.Utils
{
	public static class JWTUtils
	{
		public static string GetJWTToken(IConfiguration iconfig,string email, string name)
		{
			try
			{
                //var secretKeyValue = iconfig.GetSection("JWT").GetSection("Secret").Value;
                var secretKeyValue = iconfig.GetValue<string>("JWT:Secret");
                
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyValue));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>();
                claims.Add(new Claim("email", email));
                claims.Add(new Claim("name", name));
                var tokeOptions = new JwtSecurityToken(
                        issuer: iconfig.GetValue<string>("JWT:ValidIssuer"),
                        audience: iconfig.GetValue<string>("JWT:ValidAudience"),
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signinCredentials
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return tokenString;
            }
            catch(Exception e)
			{
				Console.WriteLine($"Exception in GetJWTToken {e.Message}");
                Console.WriteLine(e);
				throw;
			}
        }
	}
}


using System;
namespace CRUDDotNet6.Models
{
	public class AuthenticationResponse
	{
		public string JWTToken { get; set; }
		public string msg { get; set; }
		public AuthenticationResponse(string token)
		{
			this.JWTToken = token;
			this.msg = "JWT expries in 1hr";
		}
	}
}


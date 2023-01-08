using System;
namespace CRUDDotNet6.Models
{
	public class AuthenticationRequest
	{
		public string Email { get; set; }
        public string Password { get; set; }

        public AuthenticationRequest(string email, string password)
		{
			this.Email = email;
			this.Password = password;
		}
	}
}


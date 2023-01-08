using System;
using CRUDDotNet6.Models;

namespace CRUDDotNet6.Services
{
	public interface IAuthenticationService
	{
		public Task<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest);
	}
}


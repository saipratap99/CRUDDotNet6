using System;
using CRUDDotNet6.Exceptions;
using CRUDDotNet6.Models;
using CRUDDotNet6.Utils;

namespace CRUDDotNet6.Services
{
	public class AuthenticationService: IAuthenticationService
	{
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _iconfig;

        public AuthenticationService(ILogger<AuthenticationService> logger, IUserService userService, IConfiguration iconfig)
		{
			this._logger = logger;
			this._userService = userService;
            this._iconfig = iconfig;
		}

		public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest)
		{
            try
            {
                this._logger.LogInformation($"Enter Services.AuthenticationService.Authenticate, AuthenticationRequest: {authenticationRequest}");
                User user = await this._userService.GetUser(authenticationRequest.Email);
                if (!BCrypt.Net.BCrypt.Verify(authenticationRequest.Password, user.Password))
                {
                    throw new BusinessException("Invalid Email or Password.");
                }
                this._logger.LogInformation($"Exit Services.AuthenticationService.Authenticate, User {user}");
                string jwtToken = JWTUtils.GetJWTToken(this._iconfig, user.Email, user.Name);
                return new AuthenticationResponse(jwtToken);
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.AuthenticationService.Authenticate, Error: {e.Message}");
                throw;
            }
        }
	}
}


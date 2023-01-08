using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDDotNet6.Exceptions;
using CRUDDotNet6.Models;
using CRUDDotNet6.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDDotNet6.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult<object>> Login([FromBody] AuthenticationRequest authenticationRequest)
        {
            try
            {
                var response = await this._authenticationService.Authenticate(authenticationRequest);
                return Ok(response);
            }
            catch (Exception e)
            {

                if (e.GetType() == typeof(BusinessException))
                    return BadRequest(e.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}


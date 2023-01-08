using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDDotNet6.Exceptions;
using CRUDDotNet6.Models;
using CRUDDotNet6.Services;
using CRUDDotNet6.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDDotNet6.Controllers.Users
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: api/values
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            try
            {
                var response = await this._userService.GetUsers();
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

        // GET api/values/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            try
            {
                var response = await this._userService.GetUser(id);
                return Ok(response);
            }
            catch(Exception e)
            {
                
                if (e.GetType() == typeof(BusinessException))
                    return BadRequest(e.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<object>> Post([FromBody]User user) 
        {
            try
            {
                var response = await this._userService.CreateUser(user);
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDDotNet6.Exceptions;
using CRUDDotNet6.Models;
using CRUDDotNet6.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDDotNet6.Controllers.Students
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<object>> Post([FromBody] Student student)
        {
            try
            {
                var response = await this._studentService.CreateStudent(student);
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}


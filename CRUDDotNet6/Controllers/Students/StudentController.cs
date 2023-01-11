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
        public async Task<ActionResult<object>> Get()
        {
            try
            {
                var students = await this._studentService.GetStudents();
                return Ok(students);
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
                var student = await this._studentService.GetStudent(id);
                return Ok(student);
            }
            catch (Exception e)
            {

                if (e.GetType() == typeof(BusinessException))
                    return BadRequest(e.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // POST api/values
        [Authorize]
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
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> Put(int id, [FromBody]Student student)
        {
            try
            {
                var response = await this._studentService.UpdateStudent(id, student);
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

        // DELETE api/values/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            try
            {
                var response = await this._studentService.DeleteStudent(id);
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


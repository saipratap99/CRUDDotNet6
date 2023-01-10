using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDDotNet6.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCApp.Controllers
{
    public class StudentController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            Student s1 = new Student();
            s1.Id = 10;
            s1.Email = "pratap.sai99@gmail.com";
            s1.City = "Vijayawada";
            s1.Name = "Sai Pratap";

            ICollection<Student> students = new List<Student>();
            students.Add(s1);

            return View(students);
        }
    }
}


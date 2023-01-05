using System;
using CRUDDotNet6.Controllers;
using CRUDDotNet6.Models;
using MySqlConnector;

namespace CRUDDotNet6.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly studentsDBContext _context;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(studentsDBContext context, ILogger<StudentRepository> logger)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task<string> CreateStudent(Student student)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.StudentRepository.CreateStudent, Student: {student}");
                this._context.Add(student);
                int result = await this._context.SaveChangesAsync();
                this._logger.LogInformation($"Exit: Repositories.StudentRepository.CreateStudent");
                return Constants.Constants.STUDENT_CREATED_SUCCESSFULLY;
            }
            catch(MySqlException e)
            { 
                this._logger.LogInformation($"Error Repositories.StudentRepository.CreateStudent, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<string> DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Student>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateStudent(int id, Student student)
        {
            throw new NotImplementedException();
        }
    }
}


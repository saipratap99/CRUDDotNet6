using System;
using System.ComponentModel.DataAnnotations;
using CRUDDotNet6.Exceptions;
using CRUDDotNet6.Models;
using CRUDDotNet6.Repositories;
using CRUDDotNet6.Utils;

namespace CRUDDotNet6.Services
{
	public class StudentService: IStudentService
	{
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository studentRepository, ILogger<StudentService> logger)
		{
            this._logger = logger;
            this._studentRepository = studentRepository;
        }

        public async Task<string> CreateStudent(Student student)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.StudentService.CreateStudent, Student: {student}");
                ICollection<ValidationResult> results;
                if (!Helper.Validate<Student>(student, out results))
                {
                    string errorMessages = String.Join("\n", results.Select(o => o.ErrorMessage));
                    throw new BusinessException(errorMessages);
                }
                this._logger.LogInformation($"Exit Services.StudentService.CreateStudent");
                string response = await this._studentRepository.CreateStudent(student);
                return response;               
            }catch(Exception e)
            {
                this._logger.LogError($"Exit: Services.StudentService.CreateStudent, Error: {e.Message}");
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


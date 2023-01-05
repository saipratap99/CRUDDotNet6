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
                this._logger.LogError($"Error: Services.StudentService.CreateStudent, Error: {e.Message}");
                throw;
            }
        }

        public async Task<string> DeleteStudent(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.StudentService.DeleteStudent, Id: {id}");
                string response = await this._studentRepository.DeleteStudent(id);
                this._logger.LogInformation($"Exit Services.StudentService.DeleteStudent");
                return response;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.StudentService.DeleteStudent, Error: {e.Message}");
                throw;
            }
        }

        public async Task<Student> GetStudent(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.StudentService.GetStudent, Id: {id}");
                Student student = await this._studentRepository.GetStudent(id);
                this._logger.LogInformation($"Exit Services.StudentService.GetStudent, Student {student}");
                return student;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.StudentService.GetStudent, Error: {e.Message}");
                throw;
            }
        }

        public async Task<List<Student>> GetStudents()
        {
            try
            {
                this._logger.LogInformation($"Enter Services.StudentService.GetStudents");
                List<Student> students = await this._studentRepository.GetStudents();
                this._logger.LogInformation($"Exit Services.StudentService.GetStudents, Student {students}");
                return students;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.StudentService.GetStudents, Error: {e.Message}");
                throw;
            }
        }

        public async Task<string> UpdateStudent(int id, Student student)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.StudentService.UpdateStudent,Id: {id}, Student: {student}");
                ICollection<ValidationResult> results;
                if (!Helper.Validate<Student>(student, out results))
                {
                    string errorMessages = String.Join("\n", results.Select(o => o.ErrorMessage));
                    throw new BusinessException(errorMessages);
                }
                string response = await this._studentRepository.UpdateStudent(id, student);
                this._logger.LogInformation($"Exit Services.StudentService.UpdateStudent");
                return response;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.StudentService.UpdateStudent, Error: {e.Message}");
                throw;
            }
        }
    }
}


using System;
using CRUDDotNet6.Controllers;
using CRUDDotNet6.Models;
using CRUDDotNet6.Constants;
using MySqlConnector;
using Microsoft.EntityFrameworkCore;
using CRUDDotNet6.Exceptions;

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
            catch(Exception e)
            { 
                this._logger.LogInformation($"Error: Repositories.StudentRepository.CreateStudent, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> DeleteStudent(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.StudentRepository.GetStudent, Id: {id}");
                var student = await this._context.Students.FirstOrDefaultAsync(student => student.Id == id);
                if (student == null)
                    throw new BusinessException($"{Constants.Constants.STUDENT_NOT_FOUND} id: {id}");
                this._context.Remove(student);
                await this._context.SaveChangesAsync();
                this._logger.LogInformation($"Exit: Repositories.StudentRepository.GetStudent");
                return Constants.Constants.STUDENT_DELETED_SUCCESSFULLY;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.StudentRepository.GetStudent, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Student> GetStudent(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.StudentRepository.GetStudent, Id: {id}");
                var student = await this._context.Students.FirstOrDefaultAsync(student => student.Id == id);
                if (student == null)
                    throw new BusinessException($"{Constants.Constants.STUDENT_NOT_FOUND} id: {id}");
                this._logger.LogInformation($"Exit: Repositories.StudentRepository.GetStudent");
                return student;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.StudentRepository.GetStudent, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<Student>> GetStudents()
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.StudentRepository.GetStudents.");
                List<Student> students = await this._context.Students.ToListAsync<Student>();
                this._logger.LogInformation($"Exit: Repositories.StudentRepository.GetStudents, Students {students}");
                return students;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.StudentRepository.GetStudents, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> UpdateStudent(int id, Student student)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.StudentRepository.UpdateStudent, Id: {id}, Student: {student}");
                var oldStudentDetails = await this._context.Students.FirstOrDefaultAsync(studentObj => studentObj.Id == id);
                if (oldStudentDetails == null)
                    throw new BusinessException($"{Constants.Constants.STUDENT_NOT_FOUND} id: {id}");
                this._context.Entry(oldStudentDetails).CurrentValues.SetValues(student);
                int result = await this._context.SaveChangesAsync();
                this._logger.LogInformation($"Exit: Repositories.StudentRepository.UpdateStudent");
                return Constants.Constants.STUDENT_UPDATED_SUCCESSFULLY;
            }
            catch (Exception e)
            {
                this._logger.LogInformation($"Error: Repositories.StudentRepository.UpdateStudent, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }
    }
}


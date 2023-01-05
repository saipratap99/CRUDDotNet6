using System;
using CRUDDotNet6.Models;

namespace CRUDDotNet6.Repositories
{
	public interface IStudentRepository
	{
		public Task<string> CreateStudent(Student student);
        public Task<List<Student>> GetStudents();
        public Task<Student> GetStudent(int id);
        public Task<string> UpdateStudent(int id, Student student);
        public Task<string> DeleteStudent(int id);
    }
}


using System;
using CRUDDotNet6.Models;

namespace CRUDDotNet6.Services
{
	public interface IUserService
	{
        public Task<string> CreateUser(User user);
        public Task<List<User>> GetUsers();
        public Task<User> GetUser(int id);
        public Task<User> GetUser(string email);
    }
}


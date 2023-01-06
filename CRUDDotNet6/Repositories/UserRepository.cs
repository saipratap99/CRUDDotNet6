using System;
using CRUDDotNet6.Exceptions;
using CRUDDotNet6.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDDotNet6.Repositories
{
	public class UserRepository: IUserRepository
	{

        private readonly studentsDBContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(studentsDBContext context, ILogger<UserRepository> logger)
		{
			this._context = context;
			this._logger = logger;
		}

        public async Task<string> CreateUser(User user)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.UserRepository.CreateUser, Student: {user}");
                this._context.Add(user);
                int result = await this._context.SaveChangesAsync();
                this._logger.LogInformation($"Exit: Repositories.StudentRepository.CreateStudent");
                return Constants.Constants.USER_CREATED_SUCCESSFULLY;
            }
            catch (Exception e)
            {
                this._logger.LogInformation($"Error: Repositories.UserRepository.CreateUser, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.UserRepository.GetUser, Id: {id}");
                var user = await this._context.Users.FirstOrDefaultAsync(user => user.Id == id);
                if (user == null)
                    throw new BusinessException($"{Constants.Constants.USER_NOT_FOUND} id: {id}");
                this._logger.LogInformation($"Exit: Repositories.UserRepository.GetUser");
                return user;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.UserRepository.GetUser, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> GetUser(string email)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.UserRepository.GetUser, email: {email}");
                var user = await this._context.Users.FirstOrDefaultAsync(user => user.Email == email);
                if (user == null)
                    throw new BusinessException($"{Constants.Constants.USER_NOT_FOUND} email: {email}");
                this._logger.LogInformation($"Exit: Repositories.UserRepository.GetUser");
                return user;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.UserRepository.GetUser, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.StudentRepository.GetUsers.");
                List<User> users = await this._context.Users.ToListAsync<User>();
                this._logger.LogInformation($"Exit: Repositories.StudentRepository.GetUsers, users {users}");
                return users;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.StudentRepository.GetUsers, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }
    }
}


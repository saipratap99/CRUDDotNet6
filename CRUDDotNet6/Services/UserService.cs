using System;
using CRUDDotNet6.Exceptions;
using System.ComponentModel.DataAnnotations;
using CRUDDotNet6.Models;
using CRUDDotNet6.Repositories;
using CRUDDotNet6.Utils;

namespace CRUDDotNet6.Services
{
	public class UserService: IUserService
	{
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            this._logger = logger;
            this._userRepository = userRepository;
        }
        public async Task<string> CreateUser(User user)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.UserService.CreateUser, User: {user}");
                ICollection<ValidationResult> results;
                if (!Helper.Validate<User>(user, out results))
                {
                    string errorMessages = String.Join("\n", results.Select(o => o.ErrorMessage));
                    throw new BusinessException(errorMessages);
                }
                this._logger.LogInformation($"Exit Services.UserService.CreateUser");
                user.Id = 0;
                // Hashing password using Bcrypt
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                string response = await this._userRepository.CreateUser(user);
                return response;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.UserService.CreateUser, Error: {e.Message}");
                throw;
            }
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.UserService.GetUser, Id: {id}");
                User user = await this._userRepository.GetUser(id);
                this._logger.LogInformation($"Exit Services.UserService.GetUser, User {user}");
                return user;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.UserService.GetUser, Error: {e.Message}");
                throw;
            }
        }

        public async Task<User> GetUser(string email)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.UserService.GetUser, Email: {email}");
                User user = await this._userRepository.GetUser(email);
                this._logger.LogInformation($"Exit Services.UserService.GetUser, User {user}");
                return user;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.UserService.GetUser, Error: {e.Message}");
                throw;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                this._logger.LogInformation($"Enter Services.UserService.GetUsers.");
                List<User> users = await this._userRepository.GetUsers();
                this._logger.LogInformation($"Exit Services.UserService.GetUsers, Users {users}");
                return users;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.UserService.GetUsers, Error: {e.Message}");
                throw;
            }
        }
    }
}


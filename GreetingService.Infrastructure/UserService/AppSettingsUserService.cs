using GreetingService.Core;
using GreetingService.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    public class AppSettingsUserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AppSettingsUserService> _logger;

        public AppSettingsUserService(IConfiguration configuration, ILogger<AppSettingsUserService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsValidUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            var storedPassword = _configuration[username];

            if (storedPassword != null && storedPassword == password)
            {
                _logger.LogInformation("Valid credentials for {username}", username);
                return true;
            }
            _logger.LogWarning("Invalid credentials for {username}", username);
            return false;
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task ApproveUserAsync(string approvalCode)
        {
            throw new NotImplementedException();
        }

        public Task RejectUserAsync(string approvalCode)
        {
            throw new NotImplementedException();
        }
    }
}

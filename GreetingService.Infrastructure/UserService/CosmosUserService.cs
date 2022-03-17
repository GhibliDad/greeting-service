using GreetingService.Core;
using GreetingService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    public class CosmosUserService : IUserService
    {
        public async Task ApproveUserAsync(string approvalCode)
        {
            throw new NotImplementedException();
        }

        public async Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsValidUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task RejectUserAsync(string approvalCode)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

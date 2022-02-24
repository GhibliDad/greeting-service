using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreetingService.Core;
using GreetingService.Core.Entities;

namespace GreetingService.Infrastructure.UserService
{
    public class HardCodedUserService : IUserService
    {
        private static IDictionary<string, string> _users = new Dictionary<string, string>()
        {
            { "towa","mrblobby" },
            { "sofia","lakrits" },
        };

        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsValidUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            if (username == null || password == null) { return false; }

            if (!_users.TryGetValue(username, out var storedPassword))              //user does not exist
                return false;

            if (!storedPassword.Equals(password))
                return false;

            return true;
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

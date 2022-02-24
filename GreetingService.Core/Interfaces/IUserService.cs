using GreetingService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Core
{
    public interface IUserService
    {
        public Task<bool> IsValidUserAsync(string username, string password);
        public bool IsValidUser(string username, string password);
        public Task<User> GetAsync(Guid id);
        public Task<IEnumerable<User>> GetAsync();
        public Task CreateAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(Guid id);
        public Task DeleteAllAsync();
    }
}

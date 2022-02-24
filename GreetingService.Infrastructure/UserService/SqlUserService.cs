using GreetingService.Core;
using GreetingService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{

    public class SqlUserService : IUserService
    {
        private readonly GreetingDbContext _greetingDbContext;

        public SqlUserService(GreetingDbContext greetingDbContext)
        {
            _greetingDbContext = greetingDbContext;
        }
        public async Task CreateAsync(User user)
        {
            await _greetingDbContext.Users.AddAsync(user);
            await _greetingDbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            var users = await _greetingDbContext.Users.ToListAsync();
            if (!users.Any())
                throw new Exception("No Users to delete");

            users.Clear();

            await _greetingDbContext.SaveChangesAsync();
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

        public Task<bool> IsValidUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

using GreetingService.Core;
using GreetingService.Core.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(string email)
        {
            var users = await _greetingDbContext.Users.ToListAsync();
            var user = await _greetingDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                throw new Exception("User does not exist");

            users.Remove(user);
            await _greetingDbContext.SaveChangesAsync();
        }

        public async Task<User> GetAsync(string email)
        {
            var user = await _greetingDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                throw new Exception("User does not exist");

            return user;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _greetingDbContext.Users.ToListAsync();
        }

        public bool IsValidUser(string username, string password)
        {
            var user = _greetingDbContext.Users.FirstOrDefault(x => x.Email.Equals(username));
            if (user != null && user.Password.Equals(password))
                return true;

            return false;
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            var user = _greetingDbContext.Users.FirstOrDefault(x => x.Email.Equals(username));
            if (user != null && user.Password.Equals(password))
                return true;

            return false;
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = _greetingDbContext.Users.FirstOrDefault(x => x.Email.Equals(user.Email));
            if (existingUser == null)
            {           
                throw new Exception("User not found");      //Consider throwing a custom not found exception instead
            }

            if (!string.IsNullOrWhiteSpace(user.Password))
                existingUser.Password = user.Password;

            if (!string.IsNullOrWhiteSpace(user.LastName))
                existingUser.LastName = user.LastName;

            if (!string.IsNullOrWhiteSpace(user.FirstName))
                existingUser.FirstName = user.FirstName;

            existingUser.Modified = DateTime.Now;
            _greetingDbContext.SaveChanges();
        }
    }
}

using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.GreetingRepository
{
    public class SqlGreetingRepository : IGreetingRepository
    {
        private readonly GreetingDbContext _greetingDbContext;

        public SqlGreetingRepository(GreetingDbContext greetingDbContext)
        {
            _greetingDbContext = greetingDbContext;
        }

        public async Task CreateAsync(Greeting greeting)
        {
            await _greetingDbContext.Greetings.AddAsync(greeting);
            await _greetingDbContext.SaveChangesAsync();
        }
        public async Task<Greeting> GetAsync(Guid id)
        {
            var greeting = await _greetingDbContext.Greetings.FirstOrDefaultAsync(x => x.Id == id);

            return greeting;
        }

        public async Task<IEnumerable<Greeting>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Greeting greeting)
        {
            var oldGreeting = await _greetingDbContext.Greetings.FirstOrDefaultAsync(x => x.Id == greeting.Id);
            if (oldGreeting == null)
                throw new Exception("Greeting not found");

            oldGreeting.Message = greeting.Message;
            oldGreeting.To = greeting.To;
            oldGreeting.From = greeting.From;

            await _greetingDbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}

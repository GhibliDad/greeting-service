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
            greeting.Posts.Add(
                new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
            db.SaveChanges(); ;

            var oldGreeting = await GetAsync(greeting.Id);

            var oldGreetingPath = $"{oldGreeting.From}/{oldGreeting.To}/{oldGreeting.Id}";
            var oldGreetingBlobClient = _blobContainerClient.GetBlobClient(oldGreetingPath);
            await oldGreetingBlobClient.DeleteAsync();

            var newGreetingPath = $"{greeting.From}/{greeting.To}/{greeting.Id}";
            var newGreetingBinary = new BinaryData(greeting, _jsonSerializerOptions);
            var newGreetingBlobClient = _blobContainerClient.GetBlobClient(newGreetingPath);
            await newGreetingBlobClient.UploadAsync(newGreetingBinary);
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

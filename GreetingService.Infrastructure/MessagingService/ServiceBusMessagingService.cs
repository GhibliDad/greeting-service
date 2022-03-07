using GreetingService.Core.Entities;
using GreetingService.Core.Enums;
using GreetingService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    public class ServiceBusMessagingService : IMessagingService
    {
        public Task CreateAsync(Greeting greeting)
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

        public Task<Greeting> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Greeting>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Greeting>> GetAsync(string from, string to)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync<T>(T message, MessagingServiceSubject subject)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Greeting greeting)
        {
            throw new NotImplementedException();
        }
    }
}

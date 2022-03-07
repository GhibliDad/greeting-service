using GreetingService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    internal class ServiceBusMessagingService : IMessagingService
    {
        public Task SendAsync<T>(T message, string subject)
        {
            throw new NotImplementedException();
        }
    }
}

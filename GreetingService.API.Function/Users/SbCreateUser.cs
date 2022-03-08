using System;
using System.Threading.Tasks;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Users
{
    public class SbCreateUser
    {
        private readonly ILogger<SbCreateUser> _logger;
        private readonly IGreetingRepository _greetingRepository;

        public SbCreateUser(ILogger<SbCreateUser> log, IGreetingRepository greetingRepository)
        {
            _logger = log;
            _greetingRepository = greetingRepository;

        }

        [FunctionName("SbCreateUser")]
        public async Task Run([ServiceBusTrigger("main", "user_create", Connection = "ServiceBusConnectionString")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}

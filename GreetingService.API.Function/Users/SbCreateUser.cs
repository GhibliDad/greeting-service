using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Users
{
    public class SbCreateUser
    {
        private readonly ILogger<SbCreateUser> _logger;

        public SbCreateUser(ILogger<SbCreateUser> log)
        {
            _logger = log;
        }

        [FunctionName("SbCreateUser")]
        public void Run([ServiceBusTrigger("main", "user_create", Connection = "ServiceBusConnectionString")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}

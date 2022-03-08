using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Users
{
    public class SbUpdateUser
    {
        private readonly ILogger<SbUpdateUser> _logger;

        public SbUpdateUser(ILogger<SbUpdateUser> log)
        {
            _logger = log;
        }

        [FunctionName("SbUpdateUser")]
        public void Run([ServiceBusTrigger("main", "user_update", Connection = "ServiceBusConnectionString")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}

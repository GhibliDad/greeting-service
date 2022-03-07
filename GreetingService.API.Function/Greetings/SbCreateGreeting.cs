using System;
using System.Threading.Tasks;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Greetings
{
    public class SbCreateGreeting
    {
        private readonly ILogger<SbCreateGreeting> _logger;
        private readonly IGreetingRepository _greetingRepository;

        public SbCreateGreeting(ILogger<SbCreateGreeting> log)
        {
            _logger = log;
        }

        [FunctionName("SbCreateGreeting")]
        public void Run([ServiceBusTrigger("main", "greeting_create", Connection = "ServiceBusConnectionString")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}

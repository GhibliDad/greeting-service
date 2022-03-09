using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Invoices
{
    public class SbComputeInvoiceForGreeting
    {
        private readonly ILogger<SbComputeInvoiceForGreeting> _logger;

        public SbComputeInvoiceForGreeting(ILogger<SbComputeInvoiceForGreeting> log)
        {
            _logger = log;
        }

        [FunctionName("SbComputeInvoiceForGreeting")]
        public void Run([ServiceBusTrigger("main", "greeting_compute_billing", Connection = "ServiceBusConnectionString")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}

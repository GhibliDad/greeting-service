using System;
using System.Threading.Tasks;
using GreetingService.Core;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Invoices
{
    public class SbComputeInvoiceForGreeting
    {
        private readonly ILogger<SbComputeInvoiceForGreeting> _logger;
        private readonly IGreetingRepository _greetingRepository;
        private readonly IMessagingService _messagingService;
        private readonly IUserService _userService;
        private readonly IInvoiceService _invoiceService;

        public SbComputeInvoiceForGreeting(ILogger<SbComputeInvoiceForGreeting> log, IGreetingRepository greetingRepository, IMessagingService messagingService, IUserService userService, IInvoiceService invoiceService)
        {
            _logger = log;
            _greetingRepository = greetingRepository;
            _messagingService = messagingService;
            _userService = userService;
            _invoiceService = invoiceService;
        }

        [FunctionName("SbComputeInvoiceForGreeting")]
        public async Task Run([ServiceBusTrigger("main", "greeting_compute_billing", Connection = "ServiceBusConnectionString")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");

            try
            {

            }
            catch
            {

            }
        }
    }
}

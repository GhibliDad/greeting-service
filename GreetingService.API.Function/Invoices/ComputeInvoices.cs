using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using GreetingService.API.Function.Authentication;
using GreetingService.Core;
using GreetingService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Linq;
using GreetingService.Core.Entities;

namespace GreetingService.API.Function.Invoices
{
    public class ComputeInvoices
    {
        private readonly ILogger<ComputeInvoices> _logger;
        private readonly IInvoiceService _invoiceService;
        private readonly IGreetingRepository _greetingRepository;
        private readonly IUserService _userService;
        private readonly IAuthHandler _authHandler;

        public ComputeInvoices(ILogger<ComputeInvoices> log, IInvoiceService invoiceService, IGreetingRepository greetingRepository, IUserService userService, IAuthHandler authHandler)
        {
            _logger = log;
            _invoiceService = invoiceService;
            _greetingRepository = greetingRepository;
            _userService = userService;
            _authHandler = authHandler;
        }

        [FunctionName("ComputeInvoices")]
        public async Task Run([TimerTrigger("*/30 * * * * *")] TimerInfo myTimer, ILogger log)      //cron expression: */30 * * * * * means execute every 30 seconds
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var greetings = await _greetingRepository.GetAsync();

            var greetingsGroupedByInvoice = greetings.GroupBy(x => new { x.From, x.Timestamp.Year, x.Timestamp.Month });

            foreach (var group in greetingsGroupedByInvoice)
            {
                var user = await _userService.GetUserAsync(group.Key.From);
                var invoice = new Invoice
                {
                    Greetings = group,
                    Month = group.Key.Month,
                    Year = group.Key.Year,
                    Sender = user,
                };

                await _invoiceService.CreateOrUpdateInvoiceAsync(invoice);
            }
        }
    }
}


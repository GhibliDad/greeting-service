using System;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Users
{
    public class SbBeginUserApproval
    {
        private readonly ILogger<SbBeginUserApproval> _logger;
        private readonly IApprovalService _approvalService;

        public SbBeginUserApproval(ILogger<SbBeginUserApproval> log, IApprovalService approvalService)
        {
            _logger = log;
            _approvalService = approvalService;
        }

        [FunctionName("SbBeginUserApproval")]
        public async void Run([ServiceBusTrigger("main", "user_approval", Connection = "ServiceBusConnectionString")] User user)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {user}");

            try
            {
                await _approvalService.BeginUserApprovalAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed begin user approval", ex);
                throw;
            }
        }
    }
}

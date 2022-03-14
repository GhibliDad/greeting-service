using System;
using System.Collections.Generic;
using GreetingService.Core.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreetingService.Core.Entities;
using Microsoft.Extensions.Logging;
using GreetingService.Core;

namespace GreetingService.Infrastructure.ApprovalService
{
    internal class TeamsApprovalService : IApprovalService
    {
        private readonly ILogger<TeamsApprovalService> _logger;
        private readonly IGreetingRepository _greetingRepository;
        private readonly IUserService _userService;

        public TeamsApprovalService(ILogger<TeamsApprovalService> logger, IGreetingRepository greetingRepository, IUserService userService)
        {
            _logger = logger;
            _greetingRepository = greetingRepository;
            _userService = userService;
        }

        public async Task BeginUserApprovalAsync(User user)
        {
            // Please note that response body needs to be extracted and read 
            // as Connectors do not throw 429s
            try
            {
                // Perform Connector POST operation     
                var httpResponseMessage = await _client.PostAsync(IncomingWebhookUrl, new User(user));
                // Read response content
                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (responseContent.Contains("Microsoft Teams endpoint returned HTTP error 429"))
                {
                    // initiate retry logic
                }
            }
        }
    }
}

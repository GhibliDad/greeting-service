using System;
using System.Collections.Generic;
using GreetingService.Core.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreetingService.Core.Entities;
using Microsoft.Extensions.Logging;
using GreetingService.Core;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace GreetingService.Infrastructure.ApprovalService
{
    internal class TeamsApprovalService : IApprovalService
    {
        private readonly ILogger<TeamsApprovalService> _logger;
        private readonly IGreetingRepository _greetingRepository;
        private readonly IUserService _userService;
        private readonly string _teamsWebhookUrl;
        private readonly HttpClient _httpClient;

        public TeamsApprovalService(ILogger<TeamsApprovalService> logger, IGreetingRepository greetingRepository, IUserService userService, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _greetingRepository = greetingRepository;
            _userService = userService;
            _teamsWebhookUrl = configuration["TeamsWebhookUrl"];
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task BeginUserApprovalAsync(User user)
        {
            // Please note that response body needs to be extracted and read 
            // as Connectors do not throw 429s
            var json = "";

            try
            {
                // Perform Connector POST operation     
                var httpResponseMessage = await _httpClient.PostAsync(_teamsWebhookUrl, new StringContent(json));
                // Read response content
                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (responseContent.Contains("Microsoft Teams endpoint returned HTTP error 429"))
                {
                    // initiate retry logic
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something bad happened", ex);
            }
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GreetingService.API.Function.Authentication;
using GreetingService.Core.Entities;
using GreetingService.Core.Enums;
using GreetingService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;

namespace GreetingService.API.Function
{
    public class PostGreeting
    {
        private readonly ILogger<PostGreeting> _logger;
        private readonly IMessagingService _messagingService;
        private readonly IAuthHandler _authHandler;

        public PostGreeting(ILogger<PostGreeting> log, IMessagingService messagingService, IAuthHandler authHandler)
        {
            _logger = log;
            _messagingService = messagingService;
            _authHandler = authHandler;
        }

        [FunctionName("PostGreeting")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Greeting" })]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Accepted, Description = "Accepted")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "greeting")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (!await _authHandler.IsAuthorizedAsync(req))
                return new UnauthorizedResult();

            Greeting greeting;

            try
            {
                var body = await req.ReadAsStringAsync();
                greeting = JsonSerializer.Deserialize<Greeting>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            try
            {
                await _messagingService.SendAsync(greeting, MessagingServiceSubject.NewGreeting);
            }
            catch
            {
                return new ConflictResult();
            }
            
            return new AcceptedResult();
        }
    }
}


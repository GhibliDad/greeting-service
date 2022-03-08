using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
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
    public class PutGreeting
    {
        private readonly ILogger<PutGreeting> _logger;
        private readonly IGreetingRepository _greetingRepository;
        private readonly IMessagingService _messagingService;

        public PutGreeting(ILogger<PutGreeting> log, IGreetingRepository greetingRepository, IMessagingService messagingService)
        {
            _logger = log;
            _greetingRepository = greetingRepository;
            _messagingService = messagingService;
        }

        [FunctionName("PutGreeting")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Greeting" })]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Accepted, Description = "Accepted")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "greeting")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");


            Greeting greeting;

            try
            {
                var body = await req.ReadAsStringAsync();
                greeting = JsonSerializer.Deserialize<Greeting>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                //await _greetingRepository.UpdateAsync(greeting);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            try
            {
                await _messagingService.SendAsync(greeting, MessagingServiceSubject.UpdateGreeting);
            }
            catch
            {
                return new NotFoundResult();
            }

            return new AcceptedResult();
        }
    }
}


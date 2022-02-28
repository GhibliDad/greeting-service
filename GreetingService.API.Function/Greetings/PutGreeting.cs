using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GreetingService.Core.Entities;
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

        public PutGreeting(ILogger<PutGreeting> log, IGreetingRepository greetingRepository)
        {
            _logger = log;
            _greetingRepository = greetingRepository;
        }

        [FunctionName("PutGreeting")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Greeting" })]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Accepted, Description = "Accepted")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "greeting")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var body = await req.ReadAsStringAsync();
            var greeting = JsonSerializer.Deserialize<Greeting>(body);

            try
            {
                await _greetingRepository.UpdateAsync(greeting);
            }
            catch
            {
                return new NotFoundResult();
            }

            return new AcceptedResult();
        }
    }
}


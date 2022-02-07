using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using GreetingService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace GreetingService.API.Function
{
    public class DeleteGreeting
    {
        private readonly ILogger<DeleteGreeting> _logger;
        private readonly IGreetingRepository _greetingRepository;

        public DeleteGreeting(ILogger<DeleteGreeting> log, IGreetingRepository greetingRepository)
        {
            _logger = log;
            _greetingRepository = greetingRepository;
        }

        [FunctionName("DeleteGreeting")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Greeting" })]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Not found")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "greeting/{id}")] HttpRequest req, string id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (!Guid.TryParse(id, out var idGuid))
                return new BadRequestObjectResult($"{id} is not a valid Guid");

            _greetingRepository.Delete(idGuid);

            return new OkObjectResult($"Successfully deleted greeting with ID: {idGuid}!");
        }
    }
}


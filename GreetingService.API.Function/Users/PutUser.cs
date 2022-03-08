using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GreetingService.API.Function.Authentication;
using GreetingService.Core;
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

namespace GreetingService.API.Function.Users
{
    public class PutUser
    {
        private readonly ILogger<PutUser> _logger;
        private readonly IMessagingService _messagingService;
        private readonly IAuthHandler _authHandler;

        public PutUser(ILogger<PutUser> log, IMessagingService messagingService, IAuthHandler authHandler)
        {
            _logger = log;
            _messagingService = messagingService;
            _authHandler = authHandler;
        }

        [FunctionName("PutUser")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Not found")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "user")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (!await _authHandler.IsAuthorizedAsync(req))
                return new UnauthorizedResult();

            try
            {
                var body = await req.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(body);
            }
            catch
            {
                return new NotFoundResult();
            }
            try
            {

            }
            catch
            {

            }

            return new AcceptedResult();
        }
    }
}


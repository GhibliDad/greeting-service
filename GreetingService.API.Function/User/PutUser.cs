using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GreetingService.Core;
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
    public class PutUser
    {
        private readonly ILogger<PutUser> _logger;
        private readonly IUserService _userService;

        public PutUser(ILogger<PutUser> log)
        {
            _logger = log;
        }

        [FunctionName("PutUser")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Not found")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "user")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var body = await req.ReadAsStringAsync();
            var greeting = JsonSerializer.Deserialize<User>(body);

            try
            {
                await _userService.UpdateAsync(greeting);
            }
            catch
            {
                return new NotFoundResult();
            }

            return new AcceptedResult();
        }
    }
}


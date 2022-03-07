using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GreetingService.API.Function.Authentication;
using GreetingService.Core;
using GreetingService.Core.Entities;
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
    public class PostUser
    {
        private readonly ILogger<PostUser> _logger;
        private readonly IUserService _userService;
        private readonly IAuthHandler _authHandler;

        public PostUser(ILogger<PostUser> log, IUserService userService, IAuthHandler authHandler)
        {
            _logger = log;
            _userService = userService;
            _authHandler = authHandler;
        }

        [FunctionName("PostUser")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Not found")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = "user")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (!await _authHandler.IsAuthorizedAsync(req))
                return new UnauthorizedResult();

            User user;

            try
            {
                user = JsonSerializer.Deserialize<User>(req.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            { 
                return new BadRequestObjectResult(ex.Message);
            }

            await _userService.CreateUserAsync(user);

            var createdUser = await _userService.GetUserAsync(user.Email);

            return new OkObjectResult(createdUser);
        }
    }
}


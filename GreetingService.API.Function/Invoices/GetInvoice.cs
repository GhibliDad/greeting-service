using System.IO;
using System.Net;
using System.Threading.Tasks;
using GreetingService.API.Function.Authentication;
using GreetingService.Core.Helpers;
using GreetingService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace GreetingService.API.Function.Invoices
{
    public class GetInvoice
    {
        private readonly ILogger<GetInvoice> _logger;
        private readonly IInvoiceService _invoiceService;
        private readonly IAuthHandler _authHandler;

        public GetInvoice(ILogger<GetInvoice> log, IInvoiceService invoiceService, IAuthHandler authHandler)
        {
            _logger = log;
            _invoiceService = invoiceService;
            _authHandler = authHandler;
        }

        [FunctionName("GetInvoice")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Not found")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "invoice/{year}/{month}/{email}")] HttpRequest req, int year, int month, string email)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (!await _authHandler.IsAuthorizedAsync(req))
                return new UnauthorizedResult();

            if (!InputValidationHelper.IsValidEmail(email))
                return new BadRequestObjectResult($"{email} is not a valid email.");

            var invoices = await _invoiceService.GetInvoiceAsync(year, month, email);

            return new OkObjectResult(invoices);
        }
    }
}


using GreetingService.API.Function.Authentication;
using GreetingService.Core;
using GreetingService.Core.Interfaces;
using GreetingService.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

[assembly: FunctionsStartup(typeof(GreetingService.API.Function.Startup))]
namespace GreetingService.API.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddLogging();

            builder.Services.AddSingleton<IGreetingRepository, MemoryGreetingRepository>();

            builder.Services.AddScoped<IUserService, AppSettingsUserService>();

            builder.Services.AddScoped<IAuthHandler, BasicAuthHandler>();
        }
    }
}
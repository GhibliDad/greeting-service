using Azure.Messaging.ServiceBus;
using GreetingService.API.Function.Authentication;
using GreetingService.Core;
using GreetingService.Core.Interfaces;
using GreetingService.Infrastructure;
using GreetingService.Infrastructure.GreetingRepository;
using GreetingService.Infrastructure.MessagingService;
using GreetingService.Infrastructure.ApprovalService; 
using GreetingService.Infrastructure.UserService;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Serilog;
using Serilog.Sinks.AzureBlobStorage;
using System.Reflection;
using Azure.Identity;
using System;

[assembly: FunctionsStartup(typeof(GreetingService.API.Function.Startup))]
namespace GreetingService.API.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = builder.GetContext().Configuration;

            //builder.Services.AddAzureKeyVault();

            builder.Services.AddHttpClient();

            builder.Services.AddLogging();

            //Create a Serilog logger and register it as a logger
            //Get the Azure Storage Account connection string from our IConfiguration
            builder.Services.AddLogging(c =>
            {
                var connectionString = config["LogStorageAccount"];
                if (string.IsNullOrWhiteSpace(connectionString))
                    return;

                var logName = $"{Assembly.GetCallingAssembly().GetName().Name}.log";
                var logger = new LoggerConfiguration()
                                    .WriteTo.AzureBlobStorage(connectionString,
                                                              restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                                                              storageFileName: "{yyyy}/{MM}/{dd}/" + logName,
                                                              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}{Exception}")
                                    .CreateLogger();

                c.AddSerilog(logger, true);
            });

            //builder.Services.AddSingleton<IGreetingRepository, MemoryGreetingRepository>();
            //builder.Services.AddScoped<IGreetingRepository, BlobGreetingRepository>();
            builder.Services.AddScoped<IGreetingRepository, SqlGreetingRepository>();

            //builder.Services.AddScoped<IUserService, AppSettingsUserService>();
            //builder.Services.AddScoped<IUserService, BlobUserService>();
            builder.Services.AddScoped<IUserService, SqlUserService>();

            builder.Services.AddScoped<IAuthHandler, BasicAuthHandler>();

            builder.Services.AddScoped<IInvoiceService, SqlInvoiceService>();

            builder.Services.AddScoped<IMessagingService, ServiceBusMessagingService>();

            builder.Services.AddScoped<IApprovalService, TeamsApprovalService>();

            builder.Services.AddDbContext<GreetingDbContext>(options =>
            {
                options.UseSqlServer(config["GreetingDbConnectionString"]);     //make sure that the "GreetingDbConnectionString" app setting contains the connection string value
            });

            builder.Services.AddSingleton(c =>
            {
                // Create a ServiceBusClient that will authenticate using a connection string

                var serviceBusClient = new ServiceBusClient(config["ServiceBusConnectionString"]);
                return serviceBusClient.CreateSender("main");
            });
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder.AddAzureKeyVault(Environment.GetEnvironmentVariable("KeyVaultUri"));
        }
    }
}

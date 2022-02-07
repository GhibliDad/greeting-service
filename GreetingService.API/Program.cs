using GreetingService.Core;
using GreetingService.Core.Interfaces;
using GreetingService.Infrastructure;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddXmlSerializerFormatters();
                //.AddJsonOptions(o => 
                //{
                //    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                //});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//This is a new concept we haven't covered so far. Dependency Injection is built into ASP.NET. This code is executed when the application starts 
//Here we "register" i.e. we tell our application to use FileGreetingRepository as the implementation for IGreetingRepository
//We also get the FileRepositoryFilePath config value from appsettings.json and use it to construct our FileGreetingRepository
//This plumbing allows us to get the correct IGreetingRepository in our constructor in GreetingController

//builder.Services.AddScoped<IGreetingRepository, FileGreetingRepository>(c =>
//{
//    var config = c.GetService<IConfiguration>();
//    return new FileGreetingRepository(config["FileRepositoryFilePath"]);
//});

builder.Services.AddScoped<IGreetingRepository, MemoryGreetingRepository>();

builder.Services.AddScoped<IUserService, AppSettingsUserService>();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPINSIGHTS_CONNECTIONSTRING"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

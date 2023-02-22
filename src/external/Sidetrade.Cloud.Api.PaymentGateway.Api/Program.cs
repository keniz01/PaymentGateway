using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Sidetrade.Cloud.Api.PaymentGateway.Api;
using Sidetrade.Cloud.Api.PaymentGateway.Api.Helpers;
using Sidetrade.Cloud.Api.PaymentGateway.Api.Middleware;
using Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Middleware;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Configuration
    .AddJsonFile($"{Path.GetDirectoryName(ApiAssembly.GetAssemblyReference().Location)}/appsettings.json");

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(nameof(RabbitMqOptions)));
builder.Services.AddControllers()
    .AddApplicationPart(PresentationAssembly.GetAssemblyReference());
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddPresentationServices();
builder.Services.AddRateLimiting();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //options.OperationFilter<AddVendorIdHeaderParameterOperationFilter>();
    options.EnableAnnotations();
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Payment Gateway API Documentation",
            Version = "v1",
            Description = "ReDoc documentaion to show Payment Gateway API.",
            Contact = new OpenApiContact
            {
                Name = "Sidetrade support",
                Email = "support@sidetrade.com"
            },
            Extensions = new Dictionary<string, IOpenApiExtension>
            {
              {"x-logo",
                new OpenApiObject
                {
                   {"url", new OpenApiString("https://t3.ftcdn.net/jpg/05/16/28/26/240_F_516282605_eYsy2LkyY3xfHyZVJhlVUN0ClCNxV2tp.jpg")},
                   { "altText", new OpenApiString("Your logo alt text here")}
                }
              }
            }
        });
});
builder.Services.AddLogging(options =>
{
    options.AddDebug();
});
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo Documentation v1"));

    app.UseReDoc(options =>
    {
        options.DocumentTitle = "Swagger Demo Documentation";
        options.SpecUrl = "/swagger/v1/swagger.json";
    });
}

app.UseRequestContextLogger();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
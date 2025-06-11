using AzureMetadataApi.Application.Interfaces;
using AzureMetadataApi.Infrastructure.Services;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

var builder = WebApplication.CreateBuilder(args);

// Retry policy for transient faults
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

builder.Services.AddHttpClient<IMetadataService, MetadataService>()
    .AddPolicyHandler(retryPolicy);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Azure Metadata API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Azure Metadata API V1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

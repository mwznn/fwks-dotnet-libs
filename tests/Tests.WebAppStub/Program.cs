using Fwks.Tests.WebAppStub.Abstractions;
using Fwks.Tests.WebAppStub.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerGen()
    .AddScoped<IStubService, StubService>()
    .AddControllers();

builder.Services
    .AddEndpointsApiExplorer();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI();

app.MapControllers();

app.Run();

public partial class Program { }
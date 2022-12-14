using System;
using Fwks.AspNetCore.Middlewares;
using Fwks.FwksService.Core.Settings;
using Fwks.FwksService.App.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

try
{
    SerilogConfiguration.Initialize();

    var builder = WebApplication.CreateBuilder(args);

    var appSettings = builder.Configuration.Get<AppSettings>();

    builder.Host.UseSerilog();

    builder
        .Services
        .AddSingleton(appSettings)
        .AddApiConfiguration()
        .AddSecurity(appSettings)
        .AddSwagger(appSettings)
        .AddDependencies(appSettings);

    var app = builder.Build();

    app
        .UseResponseCompression()
        .UseHttpsRedirection()
        .UseSerilogRequestLogging()
        .UseSwagger(GetService<IApiVersionDescriptionProvider>(), appSettings)
        .UseRouting()
        .UseCorrelationId()
        .UseSecurity()
        .UseMiddlewares()
        .UseEndpoints(x => x.MapControllers());

    Log.Information("App is starting up.");

    app.Run();

    T GetService<T>() => app.Services.GetRequiredService<T>();
}
catch (Exception e)
{
    Log.Fatal(e, "App terminated unexpectedly.");
}
finally
{
    Log.Information("App is shutting down.");

    Log.CloseAndFlush();
}
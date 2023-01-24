using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Fwks.Tests.AspNetCore.Fixtures;

public class StubApiFixture : IDisposable
{
    private HttpClient _client;

    public void SetupClient(Action<IServiceCollection> servicesAction = default)
    {
        var factory = servicesAction == default
            ? new WebApplicationFactory<Program>()
            : new WebApplicationFactory<Program>().WithWebHostBuilder(builder => builder.ConfigureServices(servicesAction));

        var x = new WebApplicationFactory<Program>().WithWebHostBuilder(x =>
        {


        });

        _client = factory.CreateClient();
    }

    public Task<string> GetAsync(bool isDefault = false)
    {
        return _client.GetStringAsync($"stub?default={isDefault}");
    }

    public async Task ErrorAsync()
    {
        _ = await _client.GetStringAsync("stub/error");
    }

    public void Dispose()
    {
        _client.Dispose();

        GC.SuppressFinalize(this);
    }
}
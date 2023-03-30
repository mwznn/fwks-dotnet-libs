using System.Threading.Tasks;
using Fwks.Tests.WebAppStub.Abstractions;

namespace Fwks.Tests.WebAppStub.Services;

public sealed class StubService : IStubService
{
    public Task<bool> ExecuteMethodAsync()
    {
        return Task.FromResult(true);
    }
}

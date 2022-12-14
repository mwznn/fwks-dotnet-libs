using System.Threading.Tasks;

namespace Fwks.Tests.WebAppStub.Abstractions;

public interface IStubService
{
    Task<bool> ExecuteMethodAsync();
}

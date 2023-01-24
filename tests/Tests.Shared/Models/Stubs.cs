namespace Fwks.Tests.Shared.Models;

public enum EnumStub { ValueA = 0, ValueB = 1 }

public sealed class RequestStub
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public static RequestStub Fixed()
    {
        return new() { Name = "Name", Email = "customer@stub.com", Phone = "+1234567890" };
    }
}

public sealed record ResponseStub(string Value);

using System.Threading.Tasks;
using Bogus;
using Fwks.Tests.Shared.Models;
using Fwks.Tests.WebAppStub.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Fwks.Tests.WebAppStub.Controllers;

[ApiController]
[Route("[controller]")]
public class StubCtrlController : ControllerBase
{
    private readonly IStubService _service;

    public StubCtrlController(IStubService service)
    {
        _service = service;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAsync([FromQuery] bool IsFixed)
    {
        await Task.Yield();

        var response = IsFixed
            ? RequestStub.Fixed()
            : new Faker<RequestStub>()
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Email, x => x.Person.Email)
                .Generate();

        return Ok(response);
    }


    [HttpPost("")]
    public async Task<IActionResult> PostAsync([FromQuery] bool IsFixed)
    {
        await _service.ExecuteMethodAsync();

        return Ok();
    }
}

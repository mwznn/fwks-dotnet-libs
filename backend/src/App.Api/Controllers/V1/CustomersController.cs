using System.Threading.Tasks;
using Fwks.AspNetCore.Attributes;
using Fwks.FwksService.Core.Abstractions.Services;
using Fwks.FwksService.Core.Models.Requests;
using Fwks.FwksService.Core.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fwks.FwksService.App.Api.Controllers.V1;

//[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("v{v:apiVersion}/[controller]")]
public sealed class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(
        ICustomerService service)
    {
        _service = service;
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesApplicationNotificationResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromBody] AddCustomerRequest request)
    {
        await _service.AddAsync(request);

        return Accepted();
    }

    [HttpGet("")]
    [ProducesPagedResponse<CustomerResponse>(StatusCodes.Status200OK)]
    [ProducesApplicationNotificationResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromQuery] GetCustomerByNameRequest request)
    {
        return Ok(await _service.FindByFilterAsync(request));
    }
}
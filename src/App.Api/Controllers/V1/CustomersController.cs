using System.Threading.Tasks;
using Fwks.AspNetCore.Attributes;
using Fwks.FwksService.Core.Domain.Notifications;
using Fwks.FwksService.Core.Domain.Queries;
using Fwks.FwksService.Core.Domain.Responses;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fwks.FwksService.App.Api.Controllers.V1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("v{v:apiVersion}/[controller]")]
public sealed class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesApplicationNotificationResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromBody] AddCustomerNotification notification)
    {
        await _mediator.Publish(notification);

        return Accepted();
    }

    [HttpGet("")]
    [ProducesPagedResponse<CustomerResponse>(StatusCodes.Status200OK)]
    [ProducesApplicationNotificationResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromQuery] GetCustomerByNameQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}
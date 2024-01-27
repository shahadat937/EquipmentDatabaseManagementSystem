using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.PaymentStatus;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Commands;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.PaymentStatus)]
[ApiController]
[Authorize]
public class PaymentStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-PaymentStatuses")]
    public async Task<ActionResult<List<PaymentStatusDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var PaymentStatuss = await _mediator.Send(new GetPaymentStatusListRequest { QueryParams = queryParams });
        return Ok(PaymentStatuss);
    }


    [HttpGet]
    [Route("get-PaymentStatusDetail/{id}")]
    public async Task<ActionResult<PaymentStatusDto>> Get(int id)
    {
        var PaymentStatus = await _mediator.Send(new GetPaymentStatusDetailRequest { PaymentStatusId = id });
        return Ok(PaymentStatus);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-PaymentStatus")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePaymentStatusDto PaymentStatus)
    {
        var command = new CreatePaymentStatusCommand { PaymentStatusDto = PaymentStatus };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-PaymentStatus/{id}")]
    public async Task<ActionResult> Put([FromBody] PaymentStatusDto PaymentStatus)
    {
        var command = new UpdatePaymentStatusCommand { PaymentStatusDto = PaymentStatus };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-PaymentStatus/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePaymentStatusCommand { PaymentStatusId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedPaymentStatus")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedPaymentStatus()
    {
        var PaymentStatus = await _mediator.Send(new GetSelectedPaymentStatusRequest { });
        return Ok(PaymentStatus);
    }
}


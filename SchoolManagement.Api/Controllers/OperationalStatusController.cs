using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.OperationalStatuss;
using SchoolManagement.Application.Features.OperationalStatuss.Requests.Commands;
using SchoolManagement.Application.Features.OperationalStatuss.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.OperationalStatus)]
[ApiController]
[Authorize]
public class OperationalStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationalStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-OperationalStatuss")]
    public async Task<ActionResult<List<OperationalStatusDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var OperationalStatuss = await _mediator.Send(new GetOperationalStatusListRequest { QueryParams = queryParams });
        return Ok(OperationalStatuss);
    }


    [HttpGet]
    [Route("get-OperationalStatusDetail/{id}")]
    public async Task<ActionResult<OperationalStatusDto>> Get(int id)
    {
        var OperationalStatus = await _mediator.Send(new GetOperationalStatusDetailRequest { OperationalStatusId = id });
        return Ok(OperationalStatus);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-OperationalStatus")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOperationalStatusDto OperationalStatus)
    {
        var command = new CreateOperationalStatusCommand { OperationalStatusDto = OperationalStatus };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-OperationalStatus/{id}")]
    public async Task<ActionResult> Put([FromBody] OperationalStatusDto OperationalStatus)
    {
        var command = new UpdateOperationalStatusCommand { OperationalStatusDto = OperationalStatus };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-OperationalStatus/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteOperationalStatusCommand { OperationalStatusId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedOperationalStatus")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedOperationalStatus()
    {
        var OperationalStatus = await _mediator.Send(new GetSelectedOperationalStatusRequest { });
        return Ok(OperationalStatus);
    }
}


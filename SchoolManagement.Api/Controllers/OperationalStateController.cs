using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.OperationalStates;
using SchoolManagement.Application.Features.OperationalStates.Requests.Commands;
using SchoolManagement.Application.Features.OperationalStates.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.OperationalState)]
[ApiController]
[Authorize]
public class OperationalStateController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationalStateController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-OperationalStates")]
    public async Task<ActionResult<List<OperationalStateDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var OperationalStates = await _mediator.Send(new GetOperationalStateListRequest { QueryParams = queryParams });
        return Ok(OperationalStates);
    }


    [HttpGet]
    [Route("get-OperationalStateDetail/{id}")]
    public async Task<ActionResult<OperationalStateDto>> Get(int id)
    {
        var OperationalState = await _mediator.Send(new GetOperationalStateDetailRequest { OperationalStateId = id });
        return Ok(OperationalState);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-OperationalState")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOperationalStateDto OperationalState)
    {
        var command = new CreateOperationalStateCommand { OperationalStateDto = OperationalState };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-OperationalState/{id}")]
    public async Task<ActionResult> Put([FromBody] OperationalStateDto OperationalState)
    {
        var command = new UpdateOperationalStateCommand { OperationalStateDto = OperationalState };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-OperationalState/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteOperationalStateCommand { OperationalStateId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedOperationalState")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedOperationalState()
    {
        var OperationalState = await _mediator.Send(new GetSelectedOperationalStateRequest { });
        return Ok(OperationalState);
    }
}


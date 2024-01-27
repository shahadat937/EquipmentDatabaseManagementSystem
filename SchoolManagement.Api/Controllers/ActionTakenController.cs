using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ActionTaken;
using SchoolManagement.Application.Features.ActionTakens.Requests.Commands;
using SchoolManagement.Application.Features.ActionTakens.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ActionTaken)]
[ApiController]
[Authorize]
public class ActionTakenController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActionTakenController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ActionTakens")]
    public async Task<ActionResult<List<ActionTakenDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ActionTakens = await _mediator.Send(new GetActionTakenListRequest { QueryParams = queryParams });
        return Ok(ActionTakens);
    }


    [HttpGet]
    [Route("get-ActionTakenDetail/{id}")]
    public async Task<ActionResult<ActionTakenDto>> Get(int id)
    {
        var ActionTaken = await _mediator.Send(new GetActionTakenDetailRequest { ActionTakenId = id });
        return Ok(ActionTaken);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ActionTaken")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateActionTakenDto ActionTaken)
    {
        var command = new CreateActionTakenCommand { ActionTakenDto = ActionTaken };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ActionTaken/{id}")]
    public async Task<ActionResult> Put([FromBody] ActionTakenDto ActionTaken)
    {
        var command = new UpdateActionTakenCommand { ActionTakenDto = ActionTaken };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ActionTaken/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteActionTakenCommand { ActionTakenId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedActionTaken")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedActionTaken()
    {
        var ActionTaken = await _mediator.Send(new GetSelectedActionTakenRequest { });
        return Ok(ActionTaken);
    }
}


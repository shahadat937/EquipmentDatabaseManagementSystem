using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.DailyWorkState;
using SchoolManagement.Application.Features.DailyWorkStates.Requests.Commands;
using SchoolManagement.Application.Features.DailyWorkStates.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.DailyWorkState)]
[ApiController]
[Authorize]
public class DailyWorkStateController : ControllerBase
{
    private readonly IMediator _mediator;

    public DailyWorkStateController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-DailyWorkStates")]
    public async Task<ActionResult<List<DailyWorkStateDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var DailyWorkStates = await _mediator.Send(new GetDailyWorkStateListRequest { QueryParams = queryParams });
        return Ok(DailyWorkStates);
    }
    [HttpGet]
    [Route("get-dailyWorkStateListByActionTakenYes")]
    public async Task<ActionResult<List<DailyWorkStateDto>>> GetDailyWorkStateListByActionTaken()
    {
        var dailyWorkState = await _mediator.Send(new GetDailyWorkStateListForActionTakenYesRequest
        {
            //DepartmentNameId = departmentNameId
        });
        return Ok(dailyWorkState);

    }


    [HttpGet]
    [Route("get-DailyWorkStateDetail/{id}")]
    public async Task<ActionResult<DailyWorkStateDto>> Get(int id)
    {
        var DailyWorkState = await _mediator.Send(new GetDailyWorkStateDetailRequest { DailyWorkStateId = id });
        return Ok(DailyWorkState);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-DailyWorkState")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateDailyWorkStateDto DailyWorkState)
    {
        var command = new CreateDailyWorkStateCommand { DailyWorkStateDto = DailyWorkState };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-DailyWorkState/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateDailyWorkStateDto DailyWorkState)
    {
        var command = new UpdateDailyWorkStateCommand { UpdateDailyWorkStateDto = DailyWorkState };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-DailyWorkState/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDailyWorkStateCommand { DailyWorkStateId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedDailyWorkState")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDailyWorkState()
    {
        var DailyWorkState = await _mediator.Send(new GetSelectedDailyWorkStateRequest { });
        return Ok(DailyWorkState);
    }
}


using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.RunningTimes;
using SchoolManagement.Application.Features.RunningTimes.Requests.Commands;
using SchoolManagement.Application.Features.RunningTimes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.RunningTime)]
[ApiController]
[Authorize]
public class RunningTimeController : ControllerBase
{
    private readonly IMediator _mediator;

    public RunningTimeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-RunningTimes")]
    public async Task<ActionResult<List<RunningTimeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var RunningTimes = await _mediator.Send(new GetRunningTimeListRequest { QueryParams = queryParams });
        return Ok(RunningTimes);
    }


    [HttpGet]
    [Route("get-RunningTimeDetail/{id}")]
    public async Task<ActionResult<RunningTimeDto>> Get(int id)
    {
        var RunningTime = await _mediator.Send(new GetRunningTimeDetailRequest { RunningTimeId = id });
        return Ok(RunningTime);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-RunningTime")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRunningTimeDto RunningTime)
    {
        var command = new CreateRunningTimeCommand { RunningTimeDto = RunningTime };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-RunningTime/{id}")]
    public async Task<ActionResult> Put([FromBody] RunningTimeDto RunningTime)
    {
        var command = new UpdateRunningTimeCommand { RunningTimeDto = RunningTime };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-RunningTime/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteRunningTimeCommand { RunningTimeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedRunningTime")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedRunningTime()
    {
        var RunningTime = await _mediator.Send(new GetSelectedRunningTimeRequest { });
        return Ok(RunningTime);
    }
}


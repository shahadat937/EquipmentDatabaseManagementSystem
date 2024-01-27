using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime;
using SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Commands;
using SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.HalfYearlyRunningTime)]
[ApiController]
[Authorize]
public class HalfYearlyRunningTimeController : ControllerBase
{
    private readonly IMediator _mediator;

    public HalfYearlyRunningTimeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-HalfYearlyRunningTimes")]
    public async Task<ActionResult<List<HalfYearlyRunningTimeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var HalfYearlyRunningTimes = await _mediator.Send(new GetHalfYearlyRunningTimeListRequest { QueryParams = queryParams });
        return Ok(HalfYearlyRunningTimes);
    }


    [HttpGet]
    [Route("get-HalfYearlyRunningTimeDetail/{id}")]
    public async Task<ActionResult<HalfYearlyRunningTimeDto>> Get(int id)
    {
        var HalfYearlyRunningTime = await _mediator.Send(new GetHalfYearlyRunningTimeDetailRequest { HalfYearlyRunningTimeId = id });
        return Ok(HalfYearlyRunningTime);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-HalfYearlyRunningTime")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateHalfYearlyRunningTimeDto HalfYearlyRunningTime)
    {
        var command = new CreateHalfYearlyRunningTimeCommand { HalfYearlyRunningTimeDto = HalfYearlyRunningTime };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-HalfYearlyRunningTime/{id}")]
    public async Task<ActionResult> Put([FromBody] HalfYearlyRunningTimeDto HalfYearlyRunningTime)
    {
        var command = new UpdateHalfYearlyRunningTimeCommand { HalfYearlyRunningTimeDto = HalfYearlyRunningTime };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-HalfYearlyRunningTime/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteHalfYearlyRunningTimeCommand { HalfYearlyRunningTimeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedHalfYearlyRunningTime")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedHalfYearlyRunningTime()
    {
        var HalfYearlyRunningTime = await _mediator.Send(new GetSelectedHalfYearlyRunningTimeRequest { });
        return Ok(HalfYearlyRunningTime);
    }
}


using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.HalfYearlyReturn;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Commands;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Queries;
using SchoolManagement.Shared.Models;
using SendGrid;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.HalfYearlyReturn)]
[ApiController]
[Authorize]
public class HalfYearlyReturnController : ControllerBase
{
    private readonly IMediator _mediator;

    public HalfYearlyReturnController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-HalfYearlyReturns")]
    public async Task<ActionResult<List<HalfYearlyReturnDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var HalfYearlyReturns = await _mediator.Send(new GetHalfYearlyReturnListRequest { QueryParams = queryParams });
        return Ok(HalfYearlyReturns);
    }


    [HttpGet]
    [Route("get-HalfYearlyReturnDetail/{id}")]
    public async Task<ActionResult<HalfYearlyReturnDto>> Get(int id)
    {
        var HalfYearlyReturn = await _mediator.Send(new GetHalfYearlyReturnDetailRequest { HalfYearlyReturnId = id });
        return Ok(HalfYearlyReturn);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-HalfYearlyReturn")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateHalfYearlyReturnDto> HalfYearlyReturn)
    {
        var command = new CreateHalfYearlyReturnCommand { HalfYearlyReturnDto = HalfYearlyReturn };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-HalfYearlyReturn/{id}")]
    public async Task<ActionResult> Put([FromBody] HalfYearlyReturnDto HalfYearlyReturn)
    {
        var command = new UpdateHalfYearlyReturnCommand { HalfYearlyReturnDto = HalfYearlyReturn };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-HalfYearlyReturn/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteHalfYearlyReturnCommand { HalfYearlyReturnId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedHalfYearlyReturn")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedHalfYearlyReturn()
    {
        var HalfYearlyReturn = await _mediator.Send(new GetSelectedHalfYearlyReturnRequest { });
        return Ok(HalfYearlyReturn);
    }
}


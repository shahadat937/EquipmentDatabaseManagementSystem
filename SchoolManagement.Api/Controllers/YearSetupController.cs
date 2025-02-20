using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.YearSetup;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.YearSetup)]
[ApiController]
[Authorize]
public class YearSetupController : ControllerBase
{
    private readonly IMediator _mediator;

    public YearSetupController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-YearSetups")]
    public async Task<ActionResult<List<YearSetupDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var YearSetups = await _mediator.Send(new GetYearSetupListRequest { QueryParams = queryParams });
        return Ok(YearSetups);
    }


    [HttpGet]
    [Route("get-YearSetupDetail/{id}")]
    public async Task<ActionResult<YearSetupDto>> Get(int id)
    {
        var YearSetup = await _mediator.Send(new GetYearSetupDetailRequest { YearSetupId = id });
        return Ok(YearSetup);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-YearSetup")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateYearSetupDto YearSetup)
    {
        var command = new CreateYearSetupCommand { YearSetupDto = YearSetup };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-YearSetup/{id}")]
    public async Task<ActionResult> Put([FromBody] YearSetupDto YearSetup)
    {
        var command = new UpdateYearSetupCommand { YearSetupDto = YearSetup };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-YearSetup/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteYearSetupCommand { YearSetupId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedYearSetup")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedYearSetup()
    {
        var YearSetup = await _mediator.Send(new GetSelectedYearSetupRequest { });
        return Ok(YearSetup);
    }
}


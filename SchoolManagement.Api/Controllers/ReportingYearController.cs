using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ReporingYear;
using SchoolManagement.Application.DTOs.ReportingYear;
using SchoolManagement.Application.Features.ReporingYears.Requests.Commands;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ReportingYear)]
[ApiController]
[Authorize]
public class ReportingYearController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportingYearController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ReportingYears")]
    public async Task<ActionResult<List<ReportingYearDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ReporingYears = await _mediator.Send(new GetReportingYearListRequest { QueryParams = queryParams });
        return Ok(ReporingYears);
    }


    [HttpGet]
    [Route("get-ReportingYearDetail/{id}")]
    public async Task<ActionResult<ReportingYearDto>> Get(int id)
    {
        var ReporingYear = await _mediator.Send(new GetReportingYearDetailRequest { ReportingYearId = id });
        return Ok(ReporingYear);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ReportingYear")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateReportingYearDto ReporingYear)
    {
        var command = new CreateReportingYearCommand { ReportingYearDto = ReporingYear };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ReportingYear/{id}")]
    public async Task<ActionResult> Put([FromBody] ReportingYearDto ReporingYear)
    {
        var command = new UpdateReportingYearCommand { ReporingYearDto = ReporingYear };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ReportingYear/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteReportingYearCommand { ReportingYearId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedReportingYear")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedReporingYear()
    {
        var ReporingYear = await _mediator.Send(new GetSelectedReportingYearRequest { });
        return Ok(ReporingYear);
    }
}


using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ReportingMonths;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Features.ReportingMonths.Requests.Commands;
using SchoolManagement.Application.Features.ReportingMonths.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ReportingMonth)]
[ApiController]
[Authorize]
public class ReportingMonthController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportingMonthController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ReportingMonths")]
    public async Task<ActionResult<List<ReportingMonthDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ReportingMonths = await _mediator.Send(new GetReportingMonthListRequest { QueryParams = queryParams });
        return Ok(ReportingMonths);
    }


    [HttpGet]
    [Route("get-ReportingMonthDetail/{id}")]
    public async Task<ActionResult<ReportingMonthDto>> Get(int id)
    {
        var ReportingMonth = await _mediator.Send(new GetReportingMonthDetailRequest { ReportingMonthId = id });
        return Ok(ReportingMonth);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ReportingMonth")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateReportingMonthDto ReportingMonth)
    {
        var command = new CreateReportingMonthCommand { ReportingMonthDto = ReportingMonth };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ReportingMonth/{id}")]
    public async Task<ActionResult> Put([FromBody] ReportingMonthDto ReportingMonth)
    {
        var command = new UpdateReportingMonthCommand { ReportingMonthDto = ReportingMonth };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ReportingMonth/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteReportingMonthCommand { ReportingMonthId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedReportingMonth")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedReportingMonth()
    {
        var ReportingMonth = await _mediator.Send(new GetSelectedReportingMonthRequest { });
        return Ok(ReportingMonth);
    }
}


using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ReportType;
using SchoolManagement.Application.Features.ReportTypes.Requests.Commands;
using SchoolManagement.Application.Features.ReportTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ReportType)]
[ApiController]
[Authorize]
public class ReportTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ReportTypes")]
    public async Task<ActionResult<List<ReportTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ReportTypes = await _mediator.Send(new GetReportTypeListRequest { QueryParams = queryParams });
        return Ok(ReportTypes);
    }


    [HttpGet]
    [Route("get-ReportTypeDetail/{id}")]
    public async Task<ActionResult<ReportTypeDto>> Get(int id)
    {
        var ReportType = await _mediator.Send(new GetReportTypeDetailRequest { ReportTypeId = id });
        return Ok(ReportType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ReportType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateReportTypeDto ReportType)
    {
        var command = new CreateReportTypeCommand { ReportTypeDto = ReportType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ReportType/{id}")]
    public async Task<ActionResult> Put([FromBody] ReportTypeDto ReportType)
    {
        var command = new UpdateReportTypeCommand { ReportTypeDto = ReportType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ReportType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteReportTypeCommand { ReportTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedReportType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedReportType()
    {
        var ReportType = await _mediator.Send(new GetSelectedReportTypeRequest { });
        return Ok(ReportType);
    }
}


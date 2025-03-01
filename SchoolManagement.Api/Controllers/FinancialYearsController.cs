using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ReporingYear;
using SchoolManagement.Application.DTOs.FinancialYears;
using SchoolManagement.Application.Features.ReporingYears.Requests.Commands;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Shared.Models;
using SchoolManagement.Application.Features.FinancialYears.Requests.Queries;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.FinancialYears)]
[ApiController]
[Authorize]
public class FinancialYearsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FinancialYearsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-FinancialYearss")]
    public async Task<ActionResult<List<FinancialYearsDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ReporingYears = await _mediator.Send(new GetFinancialYearsListRequest { QueryParams = queryParams });
        return Ok(ReporingYears);
    }


    [HttpGet]
    [Route("get-FinancialYearsDetail/{id}")]
    public async Task<ActionResult<FinancialYearsDto>> Get(int id)
    {
        var ReporingYear = await _mediator.Send(new GetFinancialYearsDetailRequest { FinancialYearId = id });
        return Ok(ReporingYear);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-FinancialYears")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFinancialYearsDto ReporingYear)
    {
        var command = new CreateFinancialYearsCommand { FinancialYearsDto = ReporingYear };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-FinancialYears/{id}")]
    public async Task<ActionResult> Put([FromBody] FinancialYearsDto ReporingYear)
    {
        var command = new UpdateFinancialYearsCommand { ReporingYearDto = ReporingYear };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-FinancialYears/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFinancialYearsCommand { FinancialYearId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedFinancialYears")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedReporingYear()
    {
        var ReporingYear = await _mediator.Send(new GetSelectedFinancialYearsRequest { });
        return Ok(ReporingYear);
    }
}


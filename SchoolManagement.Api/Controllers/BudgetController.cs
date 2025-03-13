using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Budget;
using SchoolManagement.Application.Features.Budget.Requests.Commands;
using SchoolManagement.Application.Features.Budget.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Budget)]
[ApiController]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly IMediator _mediator;

    public BudgetController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Budget")]
    public async Task<ActionResult<List<BudgetDto>>> Get([FromQuery] QueryParams queryParams, string orderBy, string orderDirection)
    {
        var Budget = await _mediator.Send(new GetBudgetListRequest {
            QueryParams = queryParams,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        }

        );
        return Ok(Budget);
    }






    [HttpGet]
    [Route("get-BudgetDetail/{id}")]
    public async Task<ActionResult<BudgetDto>> Get(int id)
    {
        var Budget = await _mediator.Send(new GetBudgetDetailRequest { BudgetId = id });
        return Ok(Budget);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Budget")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBudgetDto Budget)
    {
        var command = new CreateBudgetCommand { BudgetDto = Budget };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Budget/{id}")]
    public async Task<ActionResult> Put([FromBody] CreateBudgetDto Budget)
    {
        var command = new UpdateBudgetCommand { BudgetDto = Budget };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Budget/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteBudgetCommand { BudgetId = id };
        await _mediator.Send(command);
        return NoContent();
    }

   

}


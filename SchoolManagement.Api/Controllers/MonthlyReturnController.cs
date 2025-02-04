using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.MonthlyReturn)]
[ApiController]
[Authorize]
public class MonthlyReturnController : ControllerBase
{
    private readonly IMediator _mediator;

    public MonthlyReturnController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-MonthlyReturns")]
    public async Task<ActionResult<List<MonthlyReturnDto>>> Get([FromQuery] QueryParams queryParams, int shipId)
    {
        var MonthlyReturns = await _mediator.Send(new GetMonthlyReturnListRequest { 
            QueryParams = queryParams,
            ShipId = shipId
            
        });
        return Ok(MonthlyReturns);
    }

    [HttpGet]
    [Route("get-MonthlyReturns-by-AuthorityId")]
    public async Task<ActionResult<List<MonthlyReturnDto>>> GetAuthorityId([FromQuery] QueryParams queryParams, int authorityId)
    {
        var MonthlyReturns = await _mediator.Send(new GetMonthlyReturnByAuthorityIdRequest { 
            QueryParams = queryParams,
            AuthorityId = authorityId
        });
        return Ok(MonthlyReturns);
    }


    [HttpGet]
    [Route("get-MonthlyReturnDetail/{id}")]
    public async Task<ActionResult<MonthlyReturnDto>> Get(int id)
    {
        var MonthlyReturn = await _mediator.Send(new GetMonthlyReturnDetailRequest { MonthlyReturnId = id });
        return Ok(MonthlyReturn);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-MonthlyReturn")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateMonthlyReturnDto MonthlyReturn)
    {
        var command = new CreateMonthlyReturnCommand { MonthlyReturnDto = MonthlyReturn };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-MonthlyReturn/{id}")]
    public async Task<ActionResult> Put([FromForm] MonthlyReturnDto monthlyReturn)
    {
        var command = new UpdateMonthlyReturnCommand { MonthlyReturnDto = monthlyReturn };
        await _mediator.Send(command);
        return NoContent();
    }



    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-MonthlyReturn/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteMonthlyReturnCommand { MonthlyReturnId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedMonthlyReturn")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedMonthlyReturn()
    {
        var MonthlyReturn = await _mediator.Send(new GetSelectedMonthlyReturnRequest { });
        return Ok(MonthlyReturn);
    }
}


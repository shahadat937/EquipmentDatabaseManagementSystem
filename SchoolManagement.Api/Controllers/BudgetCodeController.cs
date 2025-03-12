using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.BudgetCode;
using SchoolManagement.Shared.Models;
using SchoolManagement.Application.Features.BudgetCode.Requests.Queries;
using SchoolManagement.Application.Features.BudgetCode.Requests.Commands;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.BudgetCode)]
[ApiController]
[Authorize]
public class BudgetCodeController : ControllerBase
{
    private readonly IMediator _mediator;

    public BudgetCodeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-BudgetCodes")]
    public async Task<ActionResult<List<BudgetCodeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var BudgetCodes = await _mediator.Send(new GetBudgetCodeListRequest { QueryParams = queryParams });
        return Ok(BudgetCodes);
    }


    [HttpGet]
    [Route("get-BudgetCodeDetail/{id}")]
    public async Task<ActionResult<BudgetCodeDto>> Get(int id)
    {
        var BudgetCode = await _mediator.Send(new GetBudgetCodeDetailRequest { BudgetCodeId = id });
        return Ok(BudgetCode);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-BudgetCode")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBudgetCodeDto BudgetCode)
    {
        var command = new CreateBudgetCodeCommand { BudgetCodeDto = BudgetCode };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-BudgetCode/{id}")]
    public async Task<ActionResult> Put([FromBody] BudgetCodeDto BudgetCode)
    {
        var command = new UpdateBudgetCodeCommand { BudgetCode = BudgetCode };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-BudgetCode/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteBudgetCodeCommand { BudgetCodeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedBudgetCode")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedBudgetCode()
    {
        var BudgetCode = await _mediator.Send(new GetSelectedBudgetCodeRequest { });
        return Ok(BudgetCode);
    }
}


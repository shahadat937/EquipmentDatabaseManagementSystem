using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes;
using SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Commands;
using SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.QuarterlyReturnNoType)]
[ApiController]
[Authorize]
public class QuarterlyReturnNoTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuarterlyReturnNoTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-QuarterlyReturnNoTypes")]
    public async Task<ActionResult<List<QuarterlyReturnNoTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var QuarterlyReturnNoTypes = await _mediator.Send(new GetQuarterlyReturnNoTypeListRequest { QueryParams = queryParams });
        return Ok(QuarterlyReturnNoTypes);
    }


    [HttpGet]
    [Route("get-QuarterlyReturnNoTypeDetail/{id}")]
    public async Task<ActionResult<QuarterlyReturnNoTypeDto>> Get(int id)
    {
        var QuarterlyReturnNoType = await _mediator.Send(new GetQuarterlyReturnNoTypeDetailRequest { QuarterlyReturnNoTypeId = id });
        return Ok(QuarterlyReturnNoType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-QuarterlyReturnNoType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateQuarterlyReturnNoTypeDto QuarterlyReturnNoType)
    {
        var command = new CreateQuarterlyReturnNoTypeCommand { QuarterlyReturnNoTypeDto = QuarterlyReturnNoType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-QuarterlyReturnNoType/{id}")]
    public async Task<ActionResult> Put([FromBody] QuarterlyReturnNoTypeDto QuarterlyReturnNoType)
    {
        var command = new UpdateQuarterlyReturnNoTypeCommand { QuarterlyReturnNoTypeDto = QuarterlyReturnNoType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-QuarterlyReturnNoType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteQuarterlyReturnNoTypeCommand { QuarterlyReturnNoTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedQuarterlyReturnNoType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedQuarterlyReturnNoType()
    {
        var QuarterlyReturnNoType = await _mediator.Send(new GetSelectedQuarterlyReturnNoTypeRequest { });
        return Ok(QuarterlyReturnNoType);
    }
}


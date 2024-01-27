using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.LetterTypes;
using SchoolManagement.Application.Features.LetterTypes.Requests.Commands;
using SchoolManagement.Application.Features.LetterTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.LetterType)]
[ApiController]
[Authorize]
public class LetterTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public LetterTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-LetterTypes")]
    public async Task<ActionResult<List<LetterTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var LetterTypes = await _mediator.Send(new GetLetterTypeListRequest { QueryParams = queryParams });
        return Ok(LetterTypes);
    }


    [HttpGet]
    [Route("get-LetterTypeDetail/{id}")]
    public async Task<ActionResult<LetterTypeDto>> Get(int id)
    {
        var LetterType = await _mediator.Send(new GetLetterTypeDetailRequest { LetterTypeId = id });
        return Ok(LetterType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-LetterType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLetterTypeDto LetterType)
    {
        var command = new CreateLetterTypeCommand { LetterTypeDto = LetterType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-LetterType/{id}")]
    public async Task<ActionResult> Put([FromBody] LetterTypeDto LetterType)
    {
        var command = new UpdateLetterTypeCommand { LetterTypeDto = LetterType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-LetterType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLetterTypeCommand { LetterTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedLetterType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedLetterType()
    {
        var LetterType = await _mediator.Send(new GetSelectedLetterTypeRequest { });
        return Ok(LetterType);
    }
}


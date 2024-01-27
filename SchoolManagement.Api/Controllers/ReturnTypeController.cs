using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ReturnTypes;
using SchoolManagement.Application.Features.ReturnTypes.Requests.Commands;
using SchoolManagement.Application.Features.ReturnTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ReturnType)]
[ApiController]
[Authorize]
public class ReturnTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReturnTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ReturnTypes")]
    public async Task<ActionResult<List<ReturnTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ReturnTypes = await _mediator.Send(new GetReturnTypeListRequest { QueryParams = queryParams });
        return Ok(ReturnTypes);
    }


    [HttpGet]
    [Route("get-ReturnTypeDetail/{id}")]
    public async Task<ActionResult<ReturnTypeDto>> Get(int id)
    {
        var ReturnType = await _mediator.Send(new GetReturnTypeDetailRequest { ReturnTypeId = id });
        return Ok(ReturnType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ReturnType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateReturnTypeDto ReturnType)
    {
        var command = new CreateReturnTypeCommand { ReturnTypeDto = ReturnType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ReturnType/{id}")]
    public async Task<ActionResult> Put([FromBody] ReturnTypeDto ReturnType)
    {
        var command = new UpdateReturnTypeCommand { ReturnTypeDto = ReturnType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ReturnType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteReturnTypeCommand { ReturnTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedReturnType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedReturnType()
    {
        var ReturnType = await _mediator.Send(new GetSelectedReturnTypeRequest { });
        return Ok(ReturnType);
    }
}


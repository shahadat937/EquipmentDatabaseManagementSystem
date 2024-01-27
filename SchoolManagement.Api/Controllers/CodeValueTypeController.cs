using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.CodeValueType;
using SchoolManagement.Application.Features.CodeValueTypes.Requests.Commands;
using SchoolManagement.Application.Features.CodeValueTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.CodeValueType)]
[ApiController]
[Authorize]
public class CodeValueTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CodeValueTypeController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-codeValueTypes")]
    public async Task<ActionResult<List<CodeValueTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var CodeValueTypes = await _mediator.Send(new GetCodeValueTypeListRequest { QueryParams = queryParams });
        return Ok(CodeValueTypes);
    }

    

    [HttpGet]
    [Route("get-codeValueTypeDetail/{id}")]
    public async Task<ActionResult<CodeValueTypeDto>> Get(int id)
    {
        var CodeValueType = await _mediator.Send(new GetCodeValueTypeDetailRequest { Id = id });
        return Ok(CodeValueType);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-codeValueType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCodeValueTypeDto CodeValueType)
    {
        var command = new CreateCodeValueTypeCommand { CodeValueTypeDto = CodeValueType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-codeValueType/{id}")]
    public async Task<ActionResult> Put([FromBody] CodeValueTypeDto CodeValueType)
    {
        var command = new UpdateCodeValueTypeCommand { CodeValueTypeDto = CodeValueType };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-codeValueType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteCodeValueTypeCommand { CodeValueTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedCodeValueTypes")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedCodeValueType()
    {
        var codeValueType = await _mediator.Send(new GetSelectedCodeValueTypeRequest { });
        return Ok(codeValueType);
    }

}


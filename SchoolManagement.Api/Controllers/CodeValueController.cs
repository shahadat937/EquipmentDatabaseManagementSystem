using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.CodeValues;
using SchoolManagement.Application.Features.CodeValues.Requests.Commands;
using SchoolManagement.Application.Features.CodeValues.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.CodeValues)]
[ApiController]
[Authorize]
public class CodeValueController : ControllerBase
{
    private readonly IMediator _mediator;

    public CodeValueController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-codeValues")]
    public async Task<ActionResult<List<CodeValueDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var CodeValuees = await _mediator.Send(new GetCodeValueListRequest { QueryParams = queryParams });
        return Ok(CodeValuees);
    }

    

    [HttpGet]
    [Route("get-codeValueDetail/{id}")]
    public async Task<ActionResult<CodeValueDto>> Get(int id)
    {
        var CodeValue = await _mediator.Send(new GetCodeValueDetailRequest { Id = id });
        return Ok(CodeValue);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-codeValue")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCodeValueDto CodeValue)
    {
        var command = new CreateCodeValueCommand { CodeValueDto = CodeValue };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-codeValue/{id}")]
    public async Task<ActionResult> Put([FromBody] CodeValueDto CodeValue)
    {
        var command = new UpdateCodeValueCommand { CodeValueDto = CodeValue };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-codeValue/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteCodeValueCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedCodeValueByTypeWithChecked")]
    public async Task<ActionResult<List<SelectedModelChecked>>> GetSelectedCodeValueByTypeWithChecked(string type)
    {
        var codeValueByTypeWithChecked = await _mediator.Send(new GetSelectedCodeValueByTypeWithChecked { Type = type });
        return Ok(codeValueByTypeWithChecked);
    }

    [HttpGet]
    [Route("get-selectedCodeValueByType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedCodeValueByType(string type)
    {
        var codeValueByType = await _mediator.Send(new GetSelectedCodeValueByType { Type = type });
        return Ok(codeValueByType);
    }

}


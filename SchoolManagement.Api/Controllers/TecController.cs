using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Tec;
using SchoolManagement.Application.Features.Tecs.Requests.Commands;
using SchoolManagement.Application.Features.Tecs.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Tec)]
[ApiController]
[Authorize]
public class TecController : ControllerBase
{
    private readonly IMediator _mediator;

    public TecController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Tecs")]
    public async Task<ActionResult<List<TecDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Tecs = await _mediator.Send(new GetTecListRequest { QueryParams = queryParams });
        return Ok(Tecs);
    }


    [HttpGet]
    [Route("get-TecDetail/{id}")]
    public async Task<ActionResult<TecDto>> Get(int id)
    {
        var Tec = await _mediator.Send(new GetTecDetailRequest { TecId = id });
        return Ok(Tec);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Tec")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTecDto Tec)
    {
        var command = new CreateTecCommand { TecDto = Tec };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Tec/{id}")]
    public async Task<ActionResult> Put([FromBody] TecDto Tec)
    {
        var command = new UpdateTecCommand { TecDto = Tec };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Tec/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteTecCommand { TecId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedTec")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedTec()
    {
        var Tec = await _mediator.Send(new GetSelectedTecRequest { });
        return Ok(Tec);
    }
}


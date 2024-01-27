using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Controlled;
using SchoolManagement.Application.Features.Controlleds.Requests.Commands;
using SchoolManagement.Application.Features.Controlleds.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Controlled)]
[ApiController]
[Authorize]
public class ControlledController : ControllerBase
{
    private readonly IMediator _mediator;

    public ControlledController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Controlleds")]
    public async Task<ActionResult<List<ControlledDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Controlleds = await _mediator.Send(new GetControlledListRequest { QueryParams = queryParams });
        return Ok(Controlleds);
    }


    [HttpGet]
    [Route("get-ControlledDetail/{id}")]
    public async Task<ActionResult<ControlledDto>> Get(int id)
    {
        var Controlled = await _mediator.Send(new GetControlledDetailRequest { ControlledId = id });
        return Ok(Controlled);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Controlled")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateControlledDto Controlled)
    {
        var command = new CreateControlledCommand { ControlledDto = Controlled };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Controlled/{id}")]
    public async Task<ActionResult> Put([FromBody] ControlledDto Controlled)
    {
        var command = new UpdateControlledCommand { ControlledDto = Controlled };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Controlled/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteControlledCommand { ControlledId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedControlled")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedControlled()
    {
        var Controlled = await _mediator.Send(new GetSelectedControlledRequest { });
        return Ok(Controlled);
    }
}


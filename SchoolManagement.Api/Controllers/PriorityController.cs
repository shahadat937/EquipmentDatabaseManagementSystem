using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Priority;
using SchoolManagement.Application.Features.Prioritys.Requests.Commands;
using SchoolManagement.Application.Features.Prioritys.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Priority)]
[ApiController]
[Authorize]
public class PriorityController : ControllerBase
{
    private readonly IMediator _mediator;

    public PriorityController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Priorities")]
    public async Task<ActionResult<List<PriorityDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Prioritys = await _mediator.Send(new GetPriorityListRequest { QueryParams = queryParams });
        return Ok(Prioritys);
    }


    [HttpGet]
    [Route("get-PriorityDetail/{id}")]
    public async Task<ActionResult<PriorityDto>> Get(int id)
    {
        var Priority = await _mediator.Send(new GetPriorityDetailRequest { PriorityId = id });
        return Ok(Priority);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Priority")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePriorityDto Priority)
    {
        var command = new CreatePriorityCommand { PriorityDto = Priority };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Priority/{id}")]
    public async Task<ActionResult> Put([FromBody] PriorityDto Priority)
    {
        var command = new UpdatePriorityCommand { PriorityDto = Priority };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Priority/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePriorityCommand { PriorityId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedPriority")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedPriority()
    {
        var Priority = await _mediator.Send(new GetSelectedPriorityRequest { });
        return Ok(Priority);
    }
}


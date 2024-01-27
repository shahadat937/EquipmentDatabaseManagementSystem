using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Sqns;
using SchoolManagement.Application.Features.Sqns.Requests.Commands;
using SchoolManagement.Application.Features.Sqns.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Sqn)]
[ApiController]
[Authorize]
public class SqnController : ControllerBase
{
    private readonly IMediator _mediator;

    public SqnController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Sqns")]
    public async Task<ActionResult<List<SqnDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Sqns = await _mediator.Send(new GetSqnListRequest { QueryParams = queryParams });
        return Ok(Sqns);
    }


    [HttpGet]
    [Route("get-SqnDetail/{id}")]
    public async Task<ActionResult<SqnDto>> Get(int id)
    {
        var Sqn = await _mediator.Send(new GetSqnDetailRequest { SqnId = id });
        return Ok(Sqn);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Sqn")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSqnDto Sqn)
    {
        var command = new CreateSqnCommand { SqnDto = Sqn };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Sqn/{id}")]
    public async Task<ActionResult> Put([FromBody] SqnDto Sqn)
    {
        var command = new UpdateSqnCommand { SqnDto = Sqn };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Sqn/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSqnCommand { SqnId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedSqn")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSqn()
    {
        var Sqn = await _mediator.Send(new GetSelectedSqnRequest { });
        return Ok(Sqn);
    }
}


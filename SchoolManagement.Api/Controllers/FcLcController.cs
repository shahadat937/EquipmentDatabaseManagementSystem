using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.FcLc;
using SchoolManagement.Application.Features.FcLcs.Requests.Commands;
using SchoolManagement.Application.Features.FcLcs.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.FcLc)]
[ApiController]
[Authorize]
public class FcLcController : ControllerBase
{
    private readonly IMediator _mediator;

    public FcLcController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-FcLcs")]
    public async Task<ActionResult<List<FcLcDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var FcLcs = await _mediator.Send(new GetFcLcListRequest { QueryParams = queryParams });
        return Ok(FcLcs);
    }


    [HttpGet]
    [Route("get-FcLcDetail/{id}")]
    public async Task<ActionResult<FcLcDto>> Get(int id)
    {
        var FcLc = await _mediator.Send(new GetFcLcDetailRequest { FcLcId = id });
        return Ok(FcLc);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-FcLc")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFcLcDto FcLc)
    {
        var command = new CreateFcLcCommand { FcLcDto = FcLc };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-FcLc/{id}")]
    public async Task<ActionResult> Put([FromBody] FcLcDto FcLc)
    {
        var command = new UpdateFcLcCommand { FcLcDto = FcLc };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-FcLc/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFcLcCommand { FcLcId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedFcLc")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedFcLc()
    {
        var FcLc = await _mediator.Send(new GetSelectedFcLcRequest { });
        return Ok(FcLc);
    }
}


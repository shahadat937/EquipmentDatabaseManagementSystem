using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ShipDrowinges;
using SchoolManagement.Application.DTOs.ShipDrowings;
using SchoolManagement.Application.Features.ShipDrowings.Requests.Commands;
using SchoolManagement.Application.Features.ShipDrowings.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ShipDrowing)]
[ApiController]
[Authorize]
public class ShipDrowingController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShipDrowingController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ShipDrowings")]
    public async Task<ActionResult<List<ShipDrowingDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ShipDrowings = await _mediator.Send(new GetShipDrowingListRequest 
        { 
            QueryParams = queryParams
        });
        return Ok(ShipDrowings);
    }

    
    [HttpGet]
    [Route("get-ShipDrowings-By-AuthorityId")]
    public async Task<ActionResult<List<ShipDrowingDto>>> GetShipDrawingByAuthorityId([FromQuery] QueryParams queryParams, int authorityId)
    {
        var ShipDrowings = await _mediator.Send(new GetShipDrowingByAuthorityIdListRequest
        { 
            QueryParams = queryParams,
            AuthorityId = authorityId
        });
        return Ok(ShipDrowings);
    }


    [HttpGet]
    [Route("get-ShipDrowingDetail/{id}")]
    public async Task<ActionResult<ShipDrowingDto>> Get(int id)
    {
        var ShipDrowing = await _mediator.Send(new GetShipDrowingDetailRequest { ShipDrowingId = id });
        return Ok(ShipDrowing);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ShipDrowing")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateShipDrowingDto ShipDrowing)
    {
        var command = new CreateShipDrowingCommand { ShipDrowingDto = ShipDrowing };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ShipDrowing/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateShipDrowingDto ShipDrowing)
    {
        var command = new UpdateShipDrowingCommand { ShipDrowingDto = ShipDrowing };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ShipDrowing/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteShipDrowingCommand { ShipDrowingId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedShipDrowing")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedShipDrowing()
    {
        var ShipDrowing = await _mediator.Send(new GetSelectedShipDrowingRequest { });
        return Ok(ShipDrowing);
    }
}


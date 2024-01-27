using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Features.ShipTypes.Requests.Commands;
using SchoolManagement.Application.Features.ShipTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ShipType)]
[ApiController]
[Authorize]
public class ShipTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShipTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ShipTypes")]
    public async Task<ActionResult<List<ShipTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ShipTypes = await _mediator.Send(new GetShipTypeListRequest { QueryParams = queryParams });
        return Ok(ShipTypes);
    }


    [HttpGet]
    [Route("get-ShipTypeDetail/{id}")]
    public async Task<ActionResult<ShipTypeDto>> Get(int id)
    {
        var ShipType = await _mediator.Send(new GetShipTypeDetailRequest { ShipTypeId = id });
        return Ok(ShipType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ShipType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateShipTypeDto ShipType)
    {
        var command = new CreateShipTypeCommand { ShipTypeDto = ShipType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ShipType/{id}")]
    public async Task<ActionResult> Put([FromBody] ShipTypeDto ShipType)
    {
        var command = new UpdateShipTypeCommand { ShipTypeDto = ShipType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ShipType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteShipTypeCommand { ShipTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedShipType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedShipType()
    {
        var ShipType = await _mediator.Send(new GetSelectedShipTypeRequest { });
        return Ok(ShipType);
    }
}


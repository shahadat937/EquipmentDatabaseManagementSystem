using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ShipInformations;
using SchoolManagement.Application.Features.ShipInformations.Requests.Commands;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ShipInformation)]
[ApiController]
[Authorize]
public class ShipInformationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShipInformationController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ship-informations")]
    public async Task<ActionResult<List<ShipInformationDto>>> Get([FromQuery] QueryParams queryParams, int shipId)
    {
        var ShipInformations = await _mediator.Send(new GetShipInformationListRequest { 
            QueryParams = queryParams,
            ShipId = shipId
        });
        return Ok(ShipInformations);
    }


    [HttpGet]
    [Route("get-ship-information-detail/{id}")]
    public async Task<ActionResult<ShipInformationDto>> Get(int id)
    {
        var ShipInformation = await _mediator.Send(new GetShipInformationDetailRequest { ShipInformationId = id });
        return Ok(ShipInformation);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ship-information")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateShipInformationDto ShipInformation)
    {
        var command = new CreateShipInformationCommand { ShipInformationDto = ShipInformation };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ship-information/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateShipInformationDto ShipInformation)
    {
        var command = new UpdateShipInformationCommand { ShipInformationDto = ShipInformation };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ship-information/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteShipInformationCommand { ShipInformationId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selected-ship-information")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedShipInformation()
    {
        var ShipInformation = await _mediator.Send(new GetSelectedShipInformationRequest { });
        return Ok(ShipInformation);
    }
}


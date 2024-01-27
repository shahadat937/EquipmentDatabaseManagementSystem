using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.StateOfEquipments;
using SchoolManagement.Application.Features.StateOfEquipments.Requests.Commands;
using SchoolManagement.Application.Features.StateOfEquipments.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.StateOfEquipment)]
[ApiController]
[Authorize]
public class StateOfEquipmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StateOfEquipmentController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-StateOfEquipments")]
    public async Task<ActionResult<List<StateOfEquipmentDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var StateOfEquipments = await _mediator.Send(new GetStateOfEquipmentListRequest { QueryParams = queryParams });
        return Ok(StateOfEquipments);
    }


    [HttpGet]
    [Route("get-StateOfEquipmentDetail/{id}")]
    public async Task<ActionResult<StateOfEquipmentDto>> Get(int id)
    {
        var StateOfEquipment = await _mediator.Send(new GetStateOfEquipmentDetailRequest { StateOfEquipmentId = id });
        return Ok(StateOfEquipment);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-StateOfEquipment")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateStateOfEquipmentDto StateOfEquipment)
    {
        var command = new CreateStateOfEquipmentCommand { StateOfEquipmentDto = StateOfEquipment };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-StateOfEquipment/{id}")]
    public async Task<ActionResult> Put([FromBody] StateOfEquipmentDto StateOfEquipment)
    {
        var command = new UpdateStateOfEquipmentCommand { StateOfEquipmentDto = StateOfEquipment };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-StateOfEquipment/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteStateOfEquipmentCommand { StateOfEquipmentId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedStateOfEquipment")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedStateOfEquipment()
    {
        var StateOfEquipment = await _mediator.Send(new GetSelectedStateOfEquipmentRequest { });
        return Ok(StateOfEquipment);
    }
}


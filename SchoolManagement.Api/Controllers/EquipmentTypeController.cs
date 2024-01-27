using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.EquipmentType;
using SchoolManagement.Application.Features.EquipmentTypes.Requests.Commands;
using SchoolManagement.Application.Features.EquipmentTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.EquipmentType)]
[ApiController]
[Authorize]
public class EquipmentTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EquipmentTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-EquipmentTypes")]
    public async Task<ActionResult<List<EquipmentTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var EquipmentTypes = await _mediator.Send(new GetEquipmentTypeListRequest { QueryParams = queryParams });
        return Ok(EquipmentTypes);
    }


    [HttpGet]
    [Route("get-EquipmentTypeDetail/{id}")]
    public async Task<ActionResult<EquipmentTypeDto>> Get(int id)
    {
        var EquipmentType = await _mediator.Send(new GetEquipmentTypeDetailRequest { EquipmentTypeId = id });
        return Ok(EquipmentType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-EquipmentType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEquipmentTypeDto EquipmentType)
    {
        var command = new CreateEquipmentTypeCommand { EquipmentTypeDto = EquipmentType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-EquipmentType/{id}")]
    public async Task<ActionResult> Put([FromBody] EquipmentTypeDto EquipmentType)
    {
        var command = new UpdateEquipmentTypeCommand { EquipmentTypeDto = EquipmentType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-EquipmentType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteEquipmentTypeCommand { EquipmentTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedEquipmentType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedEquipmentType()
    {
        var EquipmentType = await _mediator.Send(new GetSelectedEquipmentTypeRequest { });
        return Ok(EquipmentType);
    }
}


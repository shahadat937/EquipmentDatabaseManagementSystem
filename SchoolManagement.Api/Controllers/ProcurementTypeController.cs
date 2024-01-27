using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ProcurementType;
using SchoolManagement.Application.Features.ProcurementTypes.Requests.Commands;
using SchoolManagement.Application.Features.ProcurementTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ProcurementType)]
[ApiController]
[Authorize]
public class ProcurementTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProcurementTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ProcurementTypes")]
    public async Task<ActionResult<List<ProcurementTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ProcurementTypes = await _mediator.Send(new GetProcurementTypeListRequest { QueryParams = queryParams });
        return Ok(ProcurementTypes);
    }


    [HttpGet]
    [Route("get-ProcurementTypeDetail/{id}")]
    public async Task<ActionResult<ProcurementTypeDto>> Get(int id)
    {
        var ProcurementType = await _mediator.Send(new GetProcurementTypeDetailRequest { ProcurementTypeId = id });
        return Ok(ProcurementType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ProcurementType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProcurementTypeDto ProcurementType)
    {
        var command = new CreateProcurementTypeCommand { ProcurementTypeDto = ProcurementType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ProcurementType/{id}")]
    public async Task<ActionResult> Put([FromBody] ProcurementTypeDto ProcurementType)
    {
        var command = new UpdateProcurementTypeCommand { ProcurementTypeDto = ProcurementType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ProcurementType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProcurementTypeCommand { ProcurementTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedProcurementType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedProcurementType()
    {
        var ProcurementType = await _mediator.Send(new GetSelectedProcurementTypeRequest { });
        return Ok(ProcurementType);
    }
}


using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Commands;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Procurement)]
[ApiController]
[Authorize]
public class ProcurementController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProcurementController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Procurements")]
    public async Task<ActionResult<List<ProcurementDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Procurements = await _mediator.Send(new GetProcurementListRequest { QueryParams = queryParams });
        return Ok(Procurements);
    }


    [HttpGet]
    [Route("get-ProcurementDetail/{id}")]
    public async Task<ActionResult<ProcurementDto>> Get(int id)
    {
        var Procurement = await _mediator.Send(new GetProcurementDetailRequest { ProcurementId = id });
        return Ok(Procurement);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Procurement")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProcurementDto Procurement)
    {
        var command = new CreateProcurementCommand { ProcurementDto = Procurement };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Procurement/{id}")]
    public async Task<ActionResult> Put([FromBody] ProcurementDto Procurement)
    {
        var command = new UpdateProcurementCommand { ProcurementDto = Procurement };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Procurement/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProcurementCommand { ProcurementId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedProcurement")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedProcurement()
    {
        var Procurement = await _mediator.Send(new GetSelectedProcurementRequest { });
        return Ok(Procurement);
    }
}


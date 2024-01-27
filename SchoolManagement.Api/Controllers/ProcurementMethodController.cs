using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ProcurementMethod;
using SchoolManagement.Application.Features.ProcurementMethods.Requests.Commands;
using SchoolManagement.Application.Features.ProcurementMethods.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ProcurementMethod)]
[ApiController]
[Authorize]
public class ProcurementMethodController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProcurementMethodController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ProcurementMethods")]
    public async Task<ActionResult<List<ProcurementMethodDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ProcurementMethods = await _mediator.Send(new GetProcurementMethodListRequest { QueryParams = queryParams });
        return Ok(ProcurementMethods);
    }


    [HttpGet]
    [Route("get-ProcurementMethodDetail/{id}")]
    public async Task<ActionResult<ProcurementMethodDto>> Get(int id)
    {
        var ProcurementMethod = await _mediator.Send(new GetProcurementMethodDetailRequest { ProcurementMethodId = id });
        return Ok(ProcurementMethod);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ProcurementMethod")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProcurementMethodDto ProcurementMethod)
    {
        var command = new CreateProcurementMethodCommand { ProcurementMethodDto = ProcurementMethod };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ProcurementMethod/{id}")]
    public async Task<ActionResult> Put([FromBody] ProcurementMethodDto ProcurementMethod)
    {
        var command = new UpdateProcurementMethodCommand { ProcurementMethodDto = ProcurementMethod };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ProcurementMethod/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProcurementMethodCommand { ProcurementMethodId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedProcurementMethod")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedProcurementMethod()
    {
        var ProcurementMethod = await _mediator.Send(new GetSelectedProcurementMethodRequest { });
        return Ok(ProcurementMethod);
    }
}


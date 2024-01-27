using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.DealingOfficer;
using SchoolManagement.Application.Features.DealingOfficers.Requests.Commands;
using SchoolManagement.Application.Features.DealingOfficers.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.DealingOfficer)]
[ApiController]
[Authorize]
public class DealingOfficerController : ControllerBase
{
    private readonly IMediator _mediator;

    public DealingOfficerController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-DealingOfficers")]
    public async Task<ActionResult<List<DealingOfficerDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var DealingOfficers = await _mediator.Send(new GetDealingOfficerListRequest { QueryParams = queryParams });
        return Ok(DealingOfficers);
    }


    [HttpGet]
    [Route("get-DealingOfficerDetail/{id}")]
    public async Task<ActionResult<DealingOfficerDto>> Get(int id)
    {
        var DealingOfficer = await _mediator.Send(new GetDealingOfficerDetailRequest { DealingOfficerId = id });
        return Ok(DealingOfficer);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-DealingOfficer")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDealingOfficerDto DealingOfficer)
    {
        var command = new CreateDealingOfficerCommand { DealingOfficerDto = DealingOfficer };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-DealingOfficer/{id}")]
    public async Task<ActionResult> Put([FromBody] DealingOfficerDto DealingOfficer)
    {
        var command = new UpdateDealingOfficerCommand { DealingOfficerDto = DealingOfficer };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-DealingOfficer/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDealingOfficerCommand { DealingOfficerId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedDealingOfficer")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDealingOfficer()
    {
        var DealingOfficer = await _mediator.Send(new GetSelectedDealingOfficerRequest { });
        return Ok(DealingOfficer);
    }
}


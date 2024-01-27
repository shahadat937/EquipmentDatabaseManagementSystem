using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.DgdpNssd;
using SchoolManagement.Application.Features.DgdpNssds.Requests.Commands;
using SchoolManagement.Application.Features.DgdpNssds.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.DgdpNssd)]
[ApiController]
[Authorize]
public class DgdpNssdController : ControllerBase
{
    private readonly IMediator _mediator;

    public DgdpNssdController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-DgdpNssds")]
    public async Task<ActionResult<List<DgdpNssdDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var DgdpNssds = await _mediator.Send(new GetDgdpNssdListRequest { QueryParams = queryParams });
        return Ok(DgdpNssds);
    }


    [HttpGet]
    [Route("get-DgdpNssdDetail/{id}")]
    public async Task<ActionResult<DgdpNssdDto>> Get(int id)
    {
        var DgdpNssd = await _mediator.Send(new GetDgdpNssdDetailRequest { DgdpNssdId = id });
        return Ok(DgdpNssd);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-DgdpNssd")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDgdpNssdDto DgdpNssd)
    {
        var command = new CreateDgdpNssdCommand { DgdpNssdDto = DgdpNssd };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-DgdpNssd/{id}")]
    public async Task<ActionResult> Put([FromBody] DgdpNssdDto DgdpNssd)
    {
        var command = new UpdateDgdpNssdCommand { DgdpNssdDto = DgdpNssd };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-DgdpNssd/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDgdpNssdCommand { DgdpNssdId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedDgdpNssd")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedDgdpNssd()
    {
        var DgdpNssd = await _mediator.Send(new GetSelectedDgdpNssdRequest { });
        return Ok(DgdpNssd);
    }
}


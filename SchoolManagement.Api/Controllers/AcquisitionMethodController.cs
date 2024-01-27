using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.AcquisitionMethod;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Commands;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.AcquisitionMethod)]
[ApiController]
[Authorize]
public class AcquisitionMethodController : ControllerBase
{
    private readonly IMediator _mediator;

    public AcquisitionMethodController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-AcquisitionMethods")]
    public async Task<ActionResult<List<AcquisitionMethodDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var AcquisitionMethods = await _mediator.Send(new GetAcquisitionMethodListRequest { QueryParams = queryParams });
        return Ok(AcquisitionMethods);
    }


    [HttpGet]
    [Route("get-AcquisitionMethodDetail/{id}")]
    public async Task<ActionResult<AcquisitionMethodDto>> Get(int id)
    {
        var AcquisitionMethod = await _mediator.Send(new GetAcquisitionMethodDetailRequest { AcquisitionMethodId = id });
        return Ok(AcquisitionMethod);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-AcquisitionMethod")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateAcquisitionMethodDto AcquisitionMethod)
    {
        var command = new CreateAcquisitionMethodCommand { AcquisitionMethodDto = AcquisitionMethod };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-AcquisitionMethod/{id}")]
    public async Task<ActionResult> Put([FromBody] AcquisitionMethodDto AcquisitionMethod)
    {
        var command = new UpdateAcquisitionMethodCommand { AcquisitionMethodDto = AcquisitionMethod };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-AcquisitionMethod/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteAcquisitionMethodCommand { AcquisitionMethodId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedAcquisitionMethod")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedAcquisitionMethod()
    {
        var AcquisitionMethod = await _mediator.Send(new GetSelectedAcquisitionMethodRequest { });
        return Ok(AcquisitionMethod);
    }
}


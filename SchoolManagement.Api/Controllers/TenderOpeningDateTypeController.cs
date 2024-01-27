using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.TenderOpeningDateTypes;
using SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Commands;
using SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.TenderOpeningDateType)]
[ApiController]
[Authorize]
public class TenderOpeningDateTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenderOpeningDateTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-TenderOpeningDateTypes")]
    public async Task<ActionResult<List<TenderOpeningDateTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var TenderOpeningDateTypes = await _mediator.Send(new GetTenderOpeningDateTypeListRequest { QueryParams = queryParams });
        return Ok(TenderOpeningDateTypes);
    }


    [HttpGet]
    [Route("get-TenderOpeningDateTypeDetail/{id}")]
    public async Task<ActionResult<TenderOpeningDateTypeDto>> Get(int id)
    {
        var TenderOpeningDateType = await _mediator.Send(new GetTenderOpeningDateTypeDetailRequest { TenderOpeningDateTypeId = id });
        return Ok(TenderOpeningDateType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-TenderOpeningDateType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateTenderOpeningDateTypeDto TenderOpeningDateType)
    {
        var command = new CreateTenderOpeningDateTypeCommand { TenderOpeningDateTypeDto = TenderOpeningDateType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-TenderOpeningDateType/{id}")]
    public async Task<ActionResult> Put([FromBody] TenderOpeningDateTypeDto TenderOpeningDateType)
    {
        var command = new UpdateTenderOpeningDateTypeCommand { TenderOpeningDateTypeDto = TenderOpeningDateType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-TenderOpeningDateType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteTenderOpeningDateTypeCommand { TenderOpeningDateTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedTenderOpeningDateType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedTenderOpeningDateType()
    {
        var TenderOpeningDateType = await _mediator.Send(new GetSelectedTenderOpeningDateTypeRequest { });
        return Ok(TenderOpeningDateType);
    }
}


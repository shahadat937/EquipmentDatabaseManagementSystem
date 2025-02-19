using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.EqupmentNames;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Commands;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.EqupmentName)]
[ApiController]
[Authorize]
public class EqupmentNameController : ControllerBase
{
    private readonly IMediator _mediator;

    public EqupmentNameController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-EqupmentNames")]
    public async Task<ActionResult<List<EqupmentNameDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var EqupmentNames = await _mediator.Send(new GetEqupmentNameListRequest { QueryParams = queryParams });
        return Ok(EqupmentNames);
    }


    [HttpGet]
    [Route("get-EqupmentNameDetail/{id}")]
    public async Task<ActionResult<EqupmentNameDto>> Get(int id)
    {
        var EqupmentName = await _mediator.Send(new GetEqupmentNameDetailRequest { EqupmentNameId = id });
        return Ok(EqupmentName);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-EqupmentName")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEqupmentNameDto EqupmentName)
    {
        var command = new CreateEqupmentNameCommand { EqupmentNameDto = EqupmentName };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-EqupmentName/{id}")]
    public async Task<ActionResult> Put([FromBody] EqupmentNameDto EqupmentName)
    {
        var command = new UpdateEqupmentNameCommand { EqupmentNameDto = EqupmentName };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-EqupmentName/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteEqupmentNameCommand { EqupmentNameId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedEqupmentName")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedEqupmentName()
    {
        var EqupmentName = await _mediator.Send(new GetSelectedEqupmentNameRequest { });
        return Ok(EqupmentName);
    }

    [HttpGet]
    [Route("get-selectedEqupmentNameByCategory")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedEqupmentNameByCategory(int equepmentCategoryId)
    {
        var EqupmentName = await _mediator.Send(new GetSelectedEqupmentNameByCategoryRequest {
            EqepmentCategoryId = equepmentCategoryId
        });
        return Ok(EqupmentName);
    }
    [HttpGet]
    [Route("get-equipmentListWithoutPageRequest")]
    public async Task<ActionResult<List<EqupmentNameDto>>> GetGetEquipmentListWithoutPageRequest(string searchText)
    {
        var EqupmentNames = await _mediator.Send(new GetEquipmentListWithoutPageRequest
        {
            SearchText = searchText
        });
        return Ok(EqupmentNames);
    }
}


using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.EquipmentCategorys;
using SchoolManagement.Application.Features.EquipmentCategorys.Requests.Commands;
using SchoolManagement.Application.Features.EquipmentCategorys.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.EquipmentCategory)]
[ApiController]
[Authorize]
public class EquipmentCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public EquipmentCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-EquipmentCategorys")]
    public async Task<ActionResult<List<EquipmentCategoryDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var EquipmentCategorys = await _mediator.Send(new GetEquipmentCategoryListRequest { QueryParams = queryParams });
        return Ok(EquipmentCategorys);
    }


    [HttpGet]
    [Route("get-EquipmentCategoryDetail/{id}")]
    public async Task<ActionResult<EquipmentCategoryDto>> Get(int id)
    {
        var EquipmentCategory = await _mediator.Send(new GetEquipmentCategoryDetailRequest { EquipmentCategoryId = id });
        return Ok(EquipmentCategory);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-EquipmentCategory")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEquipmentCategoryDto EquipmentCategory)
    {
        var command = new CreateEquipmentCategoryCommand { EquipmentCategoryDto = EquipmentCategory };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-EquipmentCategory/{id}")]
    public async Task<ActionResult> Put([FromBody] EquipmentCategoryDto EquipmentCategory)
    {
        var command = new UpdateEquipmentCategoryCommand { EquipmentCategoryDto = EquipmentCategory };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-EquipmentCategory/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteEquipmentCategoryCommand { EquipmentCategoryId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedEquipmentCategory")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedEquipmentCategory()
    {
        var EquipmentCategory = await _mediator.Send(new GetSelectedEquipmentCategoryRequest { });
        return Ok(EquipmentCategory);
    }
}


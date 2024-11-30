using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Commands;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ShipEquipmentInfo)]
[ApiController]
[Authorize]
public class ShipEquipmentInfoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShipEquipmentInfoController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ShipEquipmentInfos")]
    public async Task<ActionResult<List<ShipEquipmentInfoDto>>> Get([FromQuery] QueryParams queryParams, int shipId)
    {
        var ShipEquipmentInfos = await _mediator.Send(new GetShipEquipmentInfoListRequest { 
            QueryParams = queryParams,
            ShipId = shipId
        });
        return Ok(ShipEquipmentInfos);
    }
    
    [HttpGet]
    [Route("get-ShipEquipmentInfos-by-authority")]
    public async Task<ActionResult<List<ShipEquipmentInfoDto>>> GetEquipmentByAuthorityId([FromQuery] QueryParams queryParams, int authorityId)
    {
        var ShipEquipmentInfos = await _mediator.Send(new GetShipEquipmentInfoByAuthorityIdListRequest { 
            QueryParams = queryParams,
            AuthorityId= authorityId
        });
        return Ok(ShipEquipmentInfos);
    }

    [HttpGet]
    [Route("get-ShipEquipmentInfos-by-CategoryId-StateOfEquipmentId")]
    public async Task<ActionResult<List<ShipEquipmentInfoDto>>> GetShipEquipmentByCategoryIdAndStateOfEquipmentId([FromQuery] QueryParams queryParams, int categoryId, int stateOfEquipmentId)
    {
        var ShipEquipmentInfos = await _mediator.Send(new GetShipEquipmentInfoByCategoryIdAndStateOfEquipmentIdRequest
        {
            QueryParams = queryParams,
            CategoryId = categoryId,
            StateOfEquipmentId = stateOfEquipmentId
        });
        return Ok(ShipEquipmentInfos);
    }

    [HttpGet]
    [Route("get-ShipEquipmentInfos-by-CategoryId-StateOfEquipmentId-commandingAreaId")]
    public async Task<ActionResult<List<ShipEquipmentInfoDto>>> GetShipEquipmentInfoByCategoryIdEquipmentStatusIdAndCommandingAreaIdRequest([FromQuery] QueryParams queryParams, int categoryId, int stateOfEquipmentId, int commandingAreaId)
    {
        var ShipEquipmentInfos = await _mediator.Send(new GetShipEquipmentInfoByCategoryIdEquipmentStatusIdAndCommandingAreaIdRequest
        {
            QueryParams = queryParams,
            CategoryId = categoryId,
            StateOfEquipmentId = stateOfEquipmentId,
            CommandingAreaId = commandingAreaId
            

        });
        return Ok(ShipEquipmentInfos);
    }


    [HttpGet]
    [Route("get-ShipEquipmentInfos-by-CategoryId-EquipmentNameId-StateOfEquipmentId")]
    public async Task<ActionResult<List<ShipEquipmentInfoDto>>> GetShipEquipmentByCategoryIdEquipmentNameAndStateOfEquipmentId([FromQuery] QueryParams queryParams, int categoryId, int equipmentNameId, int stateOfEquipmentId)
    {
        var ShipEquipmentInfos = await _mediator.Send(new GetShipEquipmentInfoByCategoryIdNameIdAndStateOfEquipmentIdRequest
        {
            QueryParams = queryParams,            
            CategoryId = categoryId,
            EquipmentNameId = equipmentNameId,
            StateOfEquipmentId = stateOfEquipmentId
        });
        return Ok(ShipEquipmentInfos);
    }

    
    [HttpGet]
    [Route("get-ShipEquipmentInfos-by-CategoryId-EquipmentNameId-StateOfEquipmentId-CommandingAreaId")]
    public async Task<ActionResult<List<ShipEquipmentInfoDto>>> GetShipEquipmentByCategoryIdEquipmentNameAndStateOfEquipmentIdAndCommandingArea([FromQuery] QueryParams queryParams, int categoryId, int equipmentNameId, int stateOfEquipmentId, int commandingAreaId)
    {
        var ShipEquipmentInfos = await _mediator.Send(new GetShipEquipmentInfoByCategoryIdNameIdAndStateOfEquipmentIdCommandAreaIdRequest
        {
            QueryParams = queryParams,            
            CategoryId = categoryId,
            EquipmentNameId = equipmentNameId,
            StateOfEquipmentId = stateOfEquipmentId,
            CommandingAreaId = commandingAreaId
            
        });
        return Ok(ShipEquipmentInfos);
    }


    [HttpGet]
    [Route("get-ShipEquipmentInfoDetail/{id}")]
    public async Task<ActionResult<ShipEquipmentInfoDto>> Get(int id)
    {
        var ShipEquipmentInfo = await _mediator.Send(new GetShipEquipmentInfoDetailRequest { ShipEquipmentInfoId = id });
        return Ok(ShipEquipmentInfo);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ShipEquipmentInfo")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateShipEquipmentInfoDto ShipEquipmentInfo)
    {
        var command = new CreateShipEquipmentInfoCommand { ShipEquipmentInfoDto = ShipEquipmentInfo };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ShipEquipmentInfo/{id}")]
    public async Task<ActionResult> Put([FromBody] ShipEquipmentInfoDto ShipEquipmentInfo)
    {
        var command = new UpdateShipEquipmentInfoCommand { ShipEquipmentInfoDto = ShipEquipmentInfo };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ShipEquipmentInfo/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteShipEquipmentInfoCommand { ShipEquipmentInfoId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedShipEquipmentInfo")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedShipEquipmentInfo()
    {
        var ShipEquipmentInfo = await _mediator.Send(new GetSelectedShipEquipmentInfoRequest { });
        return Ok(ShipEquipmentInfo);
    }

    [HttpGet]
    [Route("get-shipEquipmentInfoListForHalfYearly")]
    public async Task<ActionResult<List<ShipEquipmentInfoDto>>> GetIssueRegisterListFromStore(int equipmentCategoryId, int equpmentNameId, int shipId)
    {
        var selectedIssueRegister = await _mediator.Send(new GetShipEquipmentInfoListForHalfYearlyRequest
        {
            EquipmentCategoryId = equipmentCategoryId,
            EqupmentNameId = equpmentNameId,
            ShipId = shipId
        });
        return Ok(selectedIssueRegister);
    }

    [HttpGet]
    [Route("get-ship-equipment-count-by-category/{stateOfEquipmentId1}/{stateOfEquipmentId2}")]
    public async Task<ActionResult> GetShipEquipmentByCategory(int stateOfEquipmentId1, int stateOfEquipmentId2)
    {
        var count = await _mediator.Send(new GetShipEquipmentCountByCategoryRequest { 
        StateOfEquipmentId1 = stateOfEquipmentId1,
        StateOfEquipmentId2 = stateOfEquipmentId2
        });
        return Ok(count);
    }  
    
    [HttpGet]
    [Route("get-ship-equipment-count-by-category-commandingArea/{stateOfEquipmentId1}/{stateOfEquipmentId2}/{commandingAreaId}")]
    public async Task<ActionResult> GetShipEquipmentByCategoryAndCommandingArea(int stateOfEquipmentId1, int stateOfEquipmentId2, int commandingAreaId)
    {
        var count = await _mediator.Send(new GetShipEquipmentCountByCategoryandCommaningAreaIdRequest
        { 
        StateOfEquipmentId1 = stateOfEquipmentId1,
        StateOfEquipmentId2 = stateOfEquipmentId2,
        CommandingAreaId = commandingAreaId
        });
        return Ok(count);
    }

    [HttpGet]
    [Route("get-combat-system-equipemnt-count/{combatSystemId}/{stateOfEquipmentId1}/{stateOfEquipmentId2}")]
    public async Task<ActionResult> GetCompatSystemEequipmentCount(int combatSystemId, int stateOfEquipmentId1, int stateOfEquipmentId2)
    {
        var shipLists = await _mediator.Send(new GetCompatSystemEequipmentCountRequest
        {
           CombatSystemId = combatSystemId,
           StateOfEquipmentId1 = stateOfEquipmentId1,
           StateOfEquipmentId2 = stateOfEquipmentId2
        });
        return Ok(shipLists);
    }

     [HttpGet]
    [Route("get-combat-system-equipemnt-count-by-commandingarea/{combatSystemId}/{stateOfEquipmentId1}/{stateOfEquipmentId2}/{commandingAreaId}")]
    public async Task<ActionResult> GetCompatSystemEequipmentCountByCommandingAreaId(int combatSystemId, int stateOfEquipmentId1, int stateOfEquipmentId2, int commandingAreaID)
    {
        var shipLists = await _mediator.Send(new GetCompatSystemEequipmentCountRequestByCommandingAreaRequest
        {
           CombatSystemId = combatSystemId,
           StateOfEquipmentId1 = stateOfEquipmentId1,
           StateOfEquipmentId2 = stateOfEquipmentId2,
           CommandingAreaId = commandingAreaID
           
        });
        return Ok(shipLists);
    }



}


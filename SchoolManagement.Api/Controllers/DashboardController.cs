using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.BaseSchoolNames;
using SchoolManagement.Application.DTOs.ShipInformations;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries;
using SchoolManagement.Application.Features.CourseDurations.Requests.Queries;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Dashboard)]
[ApiController]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ship-information-by-shiptypeid")]
    public async Task<ActionResult<List<ShipInformationDto>>> GetShipInformationListByShipType(int shipTypeId)
    {
        var ShipInformations = await _mediator.Send(new GetShipInformationListByShipTypeIdRequest
        { 
            ShipTypeId =shipTypeId 
        });
        return Ok(ShipInformations);
    }


    [HttpGet]
    [Route("get-ship-information-by-baseschoolnameid")]
    public async Task<ActionResult<List<ShipInformationDto>>> GetShipInformationListByBaseSchoolName(int baseSchoolNameId)
    {
        var ShipInformations = await _mediator.Send(new GetShipInformationByBaseSchoolNameRequest
        {
            BaseSchoolNameId = baseSchoolNameId
        });
        return Ok(ShipInformations);
    }

    [HttpGet]
    [Route("get-ship-equip-list-by-baseschoolnameid")]
    public async Task<ActionResult> GetShipEquipListByBaseSchoolName(int baseSchoolNameId)
    {
        var ShipInformations = await _mediator.Send(new GetShipEquipListByBaseSchoolNameRequest
        {
            BaseSchoolNameId = baseSchoolNameId
        });
        return Ok(ShipInformations);
    }

    [HttpGet]
    [Route("get-shipinformation-byshiptype-fromprocedure")]
    public async Task<ActionResult> GetShipInformationListByType(int shipTypeId)
    {
        var shipLists = await _mediator.Send(new GetShipListFromSpRequest
        {
            ShipTypeId = shipTypeId,
        });
        return Ok(shipLists);
    }
     
    [HttpGet]
    [Route("get-basenamelistandcount")]
    public async Task<ActionResult> GetBaseNameListAndCount()
    {
        var baseNameList = await _mediator.Send(new GetBaseSchoolNameListFromSpRequest
        {

        });
        return Ok(baseNameList);
    }

    [HttpGet]
    [Route("get-shipinfobybase")]
    public async Task<ActionResult> GetShipInfoByBase(int authorityId, int operationalStatusId)
    {
        var shipLists = await _mediator.Send(new GetAllShipInfoByBaseSpRequest
        {
            AuthorityId = authorityId,
            OperationalStatusId = operationalStatusId,
        });
        return Ok(shipLists);
    }
}  


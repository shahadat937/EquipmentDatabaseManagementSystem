using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.BaseSchoolNames;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Commands;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries;
using SchoolManagement.Application.Features.CourseDurations.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.BaseSchoolNames)]
[ApiController]
[Authorize]
public class BaseSchoolNameController : ControllerBase
{
    private readonly IMediator _mediator;

    public BaseSchoolNameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-baseSchoolNames")]
    public async Task<ActionResult<List<BaseSchoolNameDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var BaseSchoolNamees = await _mediator.Send(new GetBaseSchoolNameListRequest { QueryParams = queryParams });
        return Ok(BaseSchoolNamees);
    }

    [HttpGet]
    [Route("get-baseSchoolNameDetail/{id}")]
    public async Task<ActionResult<BaseSchoolNameDto>> Get(int id)
    {
        var BaseSchoolName = await _mediator.Send(new GetBaseSchoolNameDetailRequest { Id = id });
        return Ok(BaseSchoolName);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-baseSchoolName")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateBaseSchoolNameDto BaseSchoolName)
    {
        var command = new CreateBaseSchoolNameCommand { BaseSchoolNameDto = BaseSchoolName };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-baseSchoolName/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateBaseSchoolNameDto createBaseSchoolNameDto)
    {
        var command = new UpdateBaseSchoolNameCommand { CreateBaseSchoolNameDto = createBaseSchoolNameDto };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-baseSchoolName/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteBaseSchoolNameCommand { BaseSchoolNameId = id };
        await _mediator.Send(command);
        return NoContent();
    }


    // relational data get 

    [HttpGet]
    [Route("get-selectedSchools")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedBaseSchools()
    {
        var schools = await _mediator.Send(new GetSelectedBaseSchoolNamesRequest { });
        return Ok(schools);
    }

    [HttpGet] 
    [Route("get-selectedBaseSchoolByBase")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedBaseSchoolNameByBase(int baseNameId)
    {
        var baseSchoolByBaseName = await _mediator.Send(new GetBaseSchoolNameByBaseNameIdRequest { BaseNameId = baseNameId });
        return Ok(baseSchoolByBaseName); 
    }

    [HttpGet]
    [Route("get-organizationsList")]
    public async Task<ActionResult<List<BaseSchoolNameDto>>> GetOrganizationsList()
    {
        var orgs = await _mediator.Send(new GetOrganizationListRequest { });
        return Ok(orgs);
    }

    [HttpGet]
    [Route("get-selectedOrganizations")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedOrganizations()
    {
        var selectedOrgs = await _mediator.Send(new GetSelectedOrganizationsRequest { });
        return Ok(selectedOrgs);
    }

    [HttpGet]
    [Route("get-selectedOrganizationByBranchLevel")] 
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedOrganizationByBranchLevel()
    {
        var selectedOrgs = await _mediator.Send(new GetSelectedOrganizationByBranchRequest { });
        return Ok(selectedOrgs);
    }

    [HttpGet]
    [Route("get-selectedBaseSchoolNameByBranchLevelAndThirdLevel")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedBaseSchoolByBranchLevelAndThirdLevel(int baseNameId)
    {
        var selectedBaseSchools = await _mediator.Send(new GetSelectedBaseSchoolNameByBranchLevelAndBaseNameIdRequest 
        { 
          BaseNameId = baseNameId
        });
        return Ok(selectedBaseSchools);
    }

    [HttpGet]
    [Route("get-commendingAreaByOrganization")]
    public async Task<ActionResult<List<BaseSchoolNameDto>>> GetCommendingAreaByOrganization(int firstLevel)
    {
        var commendingArea = await _mediator.Send(new GetCommendingAreaListByOrganizationRequest { FirstLevel = firstLevel });
        return Ok(commendingArea);
    }

    [HttpGet]
    [Route("get-selectedCommendingAreas")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedCommendingAreas(int firstLevel)
    {
        var SelectedCommendingArea = await _mediator.Send(new GetSelectedCommendingAreasRequest { FirstLevel = firstLevel });
        return Ok(SelectedCommendingArea);
    }

    [HttpGet]
    [Route("get-baseNameByCommendingArea")]
    public async Task<ActionResult<List<BaseSchoolNameDto>>> GetBaseNameByCommendingArea(int secondLevel)
    {
        var baseName = await _mediator.Send(new GetBaseNameListByCommendingAreaRequest { SecondLevel = secondLevel });
        return Ok(baseName);
    }


    [HttpGet]
    [Route("get-selectedBaseNames")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedBaseNames(int secondLevel)
    {
        var SelectedBaseName = await _mediator.Send(new GetSelectedBaseNamesRequest { SecondLevel = secondLevel });
        return Ok(SelectedBaseName);
    }

    [HttpGet]
    [Route("get-baseSchoolByBaseName")]
    public async Task<ActionResult<List<BaseSchoolNameDto>>> GetBaseSchoolByBaseName(int thirdLevel)
    {
        var baseSchool = await _mediator.Send(new GetBaseSchoolListByBaseNameRequest { ThirdLevel = thirdLevel });
        return Ok(baseSchool);
    }

    [HttpGet]
    [Route("get-selectedSchoolNames")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedSchoolNames(int thirdLevel)
    {
        var SelectedSchoolName = await _mediator.Send(new GetSelectedSchoolNamesRequest { ThirdLevel = thirdLevel });
        return Ok(SelectedSchoolName);
    }

    [HttpGet]
    [Route("get-selectedBaseNamesForCourse")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedBaseNamesForCourse(int branchLevel)
    {
        var SelectedBaseName = await _mediator.Send(new GetSelectedBaseNamesForCourseRequest { BranchLevel = branchLevel });
        return Ok(SelectedBaseName);
    }

    [HttpGet]
    [Route("get-selectedShipName")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedShipName()
    {
        var SelectedSchoolName = await _mediator.Send(new GetSelectedShipNameByBranchLevelRequest {  });
        return Ok(SelectedSchoolName);
    }
}


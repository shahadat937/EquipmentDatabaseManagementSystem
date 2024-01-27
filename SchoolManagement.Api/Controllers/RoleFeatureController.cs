using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.Features.RoleFeatures.Requests.Commands;
using SchoolManagement.Application.Features.RoleFeatures.Requests.Queries;
using SchoolManagement.Shared.Models;


namespace SchoolManagement.Api.Controllers;

//[Route(SMSRoutePrefix.RoleFeature)]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoleFeatureController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleFeatureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-RoleFeatures")]
    public async Task<ActionResult<List<RoleFeatureDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var RoleFeatures = await _mediator.Send(new GetRoleFeatureListRequest { QueryParams = queryParams });
        return Ok(RoleFeatures);
    }

    [HttpGet]
    [Route("get-RoleFeatureDetail")]
    public async Task<ActionResult<RoleFeatureDto>> GetRoleFeature (string RoleId, int FeatureId)
    {
        var RoleFeature = await _mediator.Send(new GetRoleFeatureDetailRequest { 
            RoleId = RoleId,
            FeatureId = FeatureId
        });
        return Ok(RoleFeature);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-RoleFeature")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRoleFeatureDto RoleFeature)
    {
        var command = new CreateRoleFeatureCommand { RoleFeatureDto = RoleFeature };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-RoleFeature")]
    public async Task<ActionResult> Put([FromBody] RoleFeatureDto RoleFeature, string roleId, int featureId)
    {
        var command = new UpdateRoleFeatureCommand { 
            RoleFeatureDto = RoleFeature,
            RoleId = roleId,
            FeatureId = featureId
        };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-RoleFeature")]
    public async Task<ActionResult> DeleteRoleFeature(string RoleId, int FeatureId)
    {
        var command = new DeleteRoleFeatureCommand { 
            RoleId = RoleId,
            FeatureId = FeatureId
        };
        await _mediator.Send(command);
        return NoContent();
    }

    

}


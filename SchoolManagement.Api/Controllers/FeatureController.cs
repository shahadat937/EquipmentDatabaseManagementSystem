using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Application.Features.AppFeature.Requests.Commands;
using SchoolManagement.Application.Features.AppFeature.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

//[Route(SMSRoutePrefix.Feature)]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FeatureController : ControllerBase
{
    private readonly IMediator _mediator;

    public FeatureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-features")]
    public async Task<ActionResult<List<FeatureDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Features = await _mediator.Send(new GetFeatureListRequest { QueryParams = queryParams });
        return Ok(Features);
    }

    [HttpGet]
    [Route("get-featureDetail/{id}")]
    public async Task<ActionResult<FeatureDto>> Get(int id)
    {
        var Feature = await _mediator.Send(new GetFeatureDetailRequest { Id = id });
        return Ok(Feature);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-feature")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFeatureDto Feature)
    {
        var command = new CreateFeatureCommand { FeatureDto = Feature };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-feature/{id}")]
    public async Task<ActionResult> Put([FromBody] FeatureDto Feature)
    {
        var command = new UpdateFeatureCommand { FeatureDto = Feature };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-feature/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFeatureCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedfeatures")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedrole()
    {
        var features = await _mediator.Send(new GetSelectedFeatureRequest { });
        return Ok(features);
    }

    [HttpGet]
    [Route("get-selectedModuleByFeature")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedModuleByFeature(string type)
    {
        var featureByModule = await _mediator.Send(new GetSelectedFeatureByTypeRequest { Type = type });
        return Ok(featureByModule);
    }
}


using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ProjectStatus;
using SchoolManagement.Application.Features.ProjectStatuses.Requests.Commands;
using SchoolManagement.Application.Features.ProjectStatuses.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ProjectStatus)]
[ApiController]
[Authorize]
public class ProjectStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-ProjectStatuses")]
    public async Task<ActionResult<List<ProjectStatusDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ProjectStatuss = await _mediator.Send(new GetProjectStatusListRequest { QueryParams = queryParams });
        return Ok(ProjectStatuss);
    }


    [HttpGet]
    [Route("get-ProjectStatusDetail/{id}")]
    public async Task<ActionResult<ProjectStatusDto>> Get(int id)
    {
        var ProjectStatus = await _mediator.Send(new GetProjectStatusDetailRequest { ProjectStatusId = id });
        return Ok(ProjectStatus);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ProjectStatus")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProjectStatusDto ProjectStatus)
    {
        var command = new CreateProjectStatusCommand { ProjectStatusDto = ProjectStatus };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ProjectStatus/{id}")]
    public async Task<ActionResult> Put([FromBody] ProjectStatusDto ProjectStatus)
    {
        var command = new UpdateProjectStatusCommand { ProjectStatusDto = ProjectStatus };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ProjectStatus/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProjectStatusCommand { ProjectStatusId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedProjectStatus")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedProjectStatus()
    {
        var ProjectStatus = await _mediator.Send(new GetSelectedProjectStatusRequest { });
        return Ok(ProjectStatus);
    }
}


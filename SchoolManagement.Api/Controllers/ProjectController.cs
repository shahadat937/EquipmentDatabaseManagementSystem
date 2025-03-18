using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Project;
using SchoolManagement.Application.Features.Project.Requests.Commands;
using SchoolManagement.Application.Features.Project.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Project)]
[ApiController]
//[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Project")]
    public async Task<ActionResult<List<ProjectDto>>> Get([FromQuery] QueryParams queryParams, string orderBy, string orderDirection)
    {
        var Project = await _mediator.Send(new GetProjectListRequest {
            QueryParams = queryParams,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        }

        );
        return Ok(Project);
    }


    [HttpGet]
    [Route("get-ProjectDetail/{id}")]
    public async Task<ActionResult<ProjectDto>> Get(int id)
    {
        var Project = await _mediator.Send(new GetProjectDetailRequest { ProjectId = id });
        return Ok(Project);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Project")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProjectDto Project)
    {
        var command = new CreateProjectCommand { ProjectDto = Project };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Project/{id}")]
    public async Task<ActionResult> Put([FromBody] CreateProjectDto Project)
    {
        var command = new UpdateProjectCommand { ProjectDto = Project };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Project/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProjectCommand { ProjectId = id };
        await _mediator.Send(command);
        return NoContent();
    }

   

}


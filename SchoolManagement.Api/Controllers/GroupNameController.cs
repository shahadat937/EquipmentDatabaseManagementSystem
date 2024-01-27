using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.GroupNames;
using SchoolManagement.Application.Features.GroupNames.Requests.Commands;
using SchoolManagement.Application.Features.GroupNames.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.GroupName)]
[ApiController]
[Authorize]
public class GroupNameController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupNameController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-GroupNames")]
    public async Task<ActionResult<List<GroupNameDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var GroupNames = await _mediator.Send(new GetGroupNameListRequest { QueryParams = queryParams });
        return Ok(GroupNames);
    }


    [HttpGet]
    [Route("get-GroupNameDetail/{id}")]
    public async Task<ActionResult<GroupNameDto>> Get(int id)
    {
        var GroupName = await _mediator.Send(new GetGroupNameDetailRequest { GroupNameId = id });
        return Ok(GroupName);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-GroupName")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGroupNameDto GroupName)
    {
        var command = new CreateGroupNameCommand { GroupNameDto = GroupName };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-GroupName/{id}")]
    public async Task<ActionResult> Put([FromBody] GroupNameDto GroupName)
    {
        var command = new UpdateGroupNameCommand { GroupNameDto = GroupName };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-GroupName/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteGroupNameCommand { GroupNameId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedGroupName")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedGroupName()
    {
        var GroupName = await _mediator.Send(new GetSelectedGroupNameRequest { });
        return Ok(GroupName);
    }
}


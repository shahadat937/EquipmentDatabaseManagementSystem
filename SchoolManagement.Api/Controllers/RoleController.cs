using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.DTOs.Role;
using SchoolManagement.Shared.Models;


namespace SchoolManagement.Api.Controllers;

//[Route(SMSRoutePrefix.Role)]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IRoleService _roleService;

    public RoleController(IMediator mediator,IRoleService roleService)
    {
        _mediator = mediator;
        _roleService = roleService;
    }

    //[HttpGet]
    //[Route("get-roles")]
    //public async Task<ActionResult<List<RoleDto>>> Get([FromQuery] QueryParams queryParams)
    //{
    //    var Roles = await _mediator.Send(new GetRoleListRequest { QueryParams = queryParams });
    //    return Ok(Roles);
    //}

    //[HttpGet]
    //[Route("get-roleDetail/{id}")]
    //public async Task<ActionResult<RoleDto>> Get(int id)
    //{
    //    var Role = await _mediator.Send(new GetRoleDetailRequest { RoleId = id });
    //    return Ok(Role);
    //}

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-role")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRoleDto model)
    {
        return Ok(await _roleService.Save("",model));

        //var command = new CreateRoleCommand { RoleDto = Role };
        //var response = await _mediator.Send(command);
        //return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-role/{id}")]
    public async Task<ActionResult> Put(string roleId, [FromBody] CreateRoleDto model)
    {
        await _roleService.Save(roleId, model);

        //var command = new UpdateRoleCommand { RoleDto = Role };
        //await _mediator.Send(command);
        return NoContent();
    }

    //[HttpDelete]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesDefaultResponseType]
    //[Route("delete-role/{id}")]
    //public async Task<ActionResult> Delete(int id)
    //{
    //    var command = new DeleteRoleCommand { RoleId = id };
    //    await _mediator.Send(command);
    //    return NoContent();
    //}

    [HttpGet]
    [Route("get-selectedroles")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedrole()
    {
        // var UserByRole = await _mediator.Send(new GetSelectedRoleRequest { });
        var UserByRole = await _roleService.GetSelectedRoleList();
        return Ok(UserByRole);
    }


    [HttpGet]
    [Route("get-selectedallroles")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedallrole()
    {
        // var UserByRole = await _mediator.Send(new GetSelectedRoleRequest { });
        var UserByRole = await _roleService.GetSelectedAllRoleList();
        return Ok(UserByRole);
    }

    [HttpGet]
    [Route("get-selectedrolesForTrainee")]
    public async Task<ActionResult<List<SelectedModel>>> getSelectedRoleForTraineeList()
    {
        // var UserByRole = await _mediator.Send(new GetSelectedRoleRequest { });
        var UserByRole = await _roleService.GetSelectedRoleForTraineeList();
        return Ok(UserByRole);
    }

}


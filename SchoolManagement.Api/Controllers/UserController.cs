using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.DTOs.User;

namespace SchoolManagement.Api.Controllers;

//[Route(SMSRoutePrefix.Users)]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    
    private readonly IUserService _userService;
    private readonly IMediator _mediator;
    public UsersController(IUserService userService, IMediator mediator)
    {
        _userService = userService;
        _mediator = mediator;
    }
   


    [HttpGet]
    [Route("get-users")]
    public async Task<ActionResult> Get([FromQuery] QueryParams queryParams)
    {
        var Users = await _userService.GetUsers(queryParams);
        return Ok(Users);
    }
    [HttpGet]
    [Route("get-teacher-users")]
    public async Task<ActionResult> GetTeacherUsers([FromQuery] QueryParams queryParams)
    {
        var Users = await _userService.GetTeacherUsers(queryParams);
        return Ok(Users);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
   // [ProducesDefaultResponseType]
    [Route("update-paswordfor-individualuser")]  
    public async Task<ActionResult> UpdatePassword([FromBody] PasswordChangeDto User)
    {
        await _userService.UpdateUserPassword(User.UserId,User);
        return NoContent();
    }

    //[HttpPut]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesDefaultResponseType]
    //[Route("update-bnaBatch/{id}")]
    //public async Task<ActionResult> Put([FromBody] BnaBatchDto BnaBatch)
    //{
    //    var command = new UpdateBnaBatchCommand { BnaBatchDto = BnaBatch };
    //    await _mediator.Send(command);
    //    return NoContent();
    //}

    [HttpGet]
    [Route("get-usersfor-passwordchange")]
    public async Task<ActionResult> GetUsersForChangePassword(string userId)
    {
        var Users = await _userService.GetEmployeeByUserId(userId);
        return Ok(Users);
    }

    [HttpPost]
    [Route("reset-userPassword")]
    public async Task<ActionResult> ResetUserPassword(string userId, [FromBody] CreateUserDto User)
    {
        var Users = await _userService.ResetPassword(userId,User);
        return NoContent();
    }
    // hoice vaiya
    //Relational data get 
    //[HttpGet]
    // GetEmployeeByUserId
    //[Route("get-selectedUsersByRole")] 
    //public async Task<ActionResult<List<SelectedModel>>> getselecteduserbyrole(string role)
    //{
    //    var UserByRole = await _mediator.Send(new GetSelectedUserByRole { Role = role });
    //    return Ok(UserByRole);
    //}


    //[HttpGet]
    //[Route("get-selectedUserByBranchInfo")]
    //public async Task<ActionResult<List<SelectedModel>>> getselecteduserbybranchinfo(string branchInfo)
    //{
    //    var UserByBranchInfo = await _mediator.Send(new GetSelectedUserByBranchInfo { BranchInfo = branchInfo });
    //    return Ok(UserByBranchInfo);
    //}

    // relational method close

    [HttpGet]
    [Route("get-userDetail/{id}")]
    public async Task<ActionResult<UserDto>> Get(string id)
    {
        var User = await  _userService.GetUserById(id);
        return Ok(User);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-user")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateUserDto User)
    {
       
        return Ok(await _userService.Save("",User));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-user")]
    public async Task<ActionResult> Put(string userId,[FromBody] CreateUserDto User)
    {
        await _userService.Save(userId,User);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-user/{id}")]
    public async Task<ActionResult<BaseCommandResponse>> Delete(string id)
    {
        await _userService.DeleteUser(id);
        return NoContent();
    }

}


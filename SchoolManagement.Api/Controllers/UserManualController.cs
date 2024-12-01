using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.UserManual;
using SchoolManagement.Application.Features.UserManuals.Requests.Commands;
using SchoolManagement.Application.Features.UserManuals.Requests.Queries;

namespace SchoolManagement.Api.Controllers
{
    [Route(SMSRoutePrefix.UserManual)]
    [ApiController]
    [Authorize]
    public class UserManualController:ControllerBase
    {
        private readonly IMediator _mediator;

        public UserManualController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-UserManuals")]
        public async Task<ActionResult<List<UserManualDto>>> Get([FromQuery] QueryParams queryParams)
        {
            var UserManuals = await _mediator.Send(new GetUserManualListRequest { QueryParams = queryParams });
            return Ok(UserManuals);
        }



        [HttpGet]
        [Route("get-UserManualDetail/{id}")]
        public async Task<ActionResult<UserManualDto>> Get(int id)
        {
            var UserManual = await _mediator.Send(new GetUserManualDetailRequest { UserManualId = id });
            return Ok(UserManual);
        }

        [HttpGet]
        [Route("get-UserManualByRole")]
        public async Task<ActionResult> Get(string UserRoleName)
        {
            var UserManual = await _mediator.Send(new GetUserManualByRoleRequest { RoleName = UserRoleName });
            return Ok(UserManual);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-UserManual")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateUserManualDto UserManual)
        {
            var command = new CreateUserManualCommand { UserManualDto = UserManual };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-UserManual/{id}")]
        public async Task<ActionResult> Put([FromForm] CreateUserManualDto createUserManualDto)
        {
            var command = new UpdateUserManualCommand { CreateUserManualDto = createUserManualDto };
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("delete-UserManual/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteUserManualCommand { UserManualId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

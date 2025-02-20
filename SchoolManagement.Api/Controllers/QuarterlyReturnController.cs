using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.QuarterlyReturns;
using SchoolManagement.Application.Features.QuarterlyReturns.Request.Commands;
using SchoolManagement.Application.Features.QuarterlyReturns.Request.Queries;

namespace SchoolManagement.Api.Controllers
{

    [Route(SMSRoutePrefix.QuarterlyReturn)]
    [ApiController]
    [Authorize]
    public class QuarterlyReturnController:ControllerBase
    {
        private readonly IMediator _mediator;

        public QuarterlyReturnController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-QuarterlyReturn")]
        public async Task<ActionResult<List<QuarterlyReturnDto>>> Get([FromQuery] QueryParams queryParams, int shipId)
        {
            var QuarterlyReturns = await _mediator.Send(new GetQuarterlyReturnListRequest { 
                QueryParams = queryParams,
                ShipId  = shipId
                
            });
            return Ok(QuarterlyReturns);
        }

        [HttpGet]
        [Route("get-QuarterlyReturn-by-AuthorityId")]
        public async Task<ActionResult<List<QuarterlyReturnDto>>> GetQuarterlyReturnByAuthorityId([FromQuery] QueryParams queryParams, int authorityId)
        {
            var QuarterlyReturns = await _mediator.Send(new GetQuarterlyReturnsByAuthorityIdRequest {
                QueryParams = queryParams,
                AuthorityId = authorityId
            });
            return Ok(QuarterlyReturns);
        }

        [HttpGet]
        [Route("get-QuarterlyReturnDetail/{id}")]
        public async Task<ActionResult<QuarterlyReturnDto>> Get(int id)
        {
            var QuarterlyReturns = await _mediator.Send(new GetQuarterlyReturnDetailRequest { QuarterlyReturnId = id });
            return Ok(QuarterlyReturns);
        }



        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-QuarterlyReturn")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateQuarterlyReturnDto QuarterlyReturn)
        {
            var command = new CreateQuarterlyReturnCommand { QuarterlyReturnDto = QuarterlyReturn };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-QuarterlyReturn/{id}")]
        public async Task<ActionResult> Put([FromForm] CreateQuarterlyReturnDto QuarterlyReturn)
        {
            var command = new UpdateQuarterlyReturnCommand { QuarterlyReturnDto = QuarterlyReturn };
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("delete-QuarterlyReturn/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteQuarterlyReturnCommand { QuarterlyReturnId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

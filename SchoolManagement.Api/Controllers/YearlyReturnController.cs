using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Queries;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Queries;
using SchoolManagement.Application.Features.YearlyReturns.Request.Commands;
using SchoolManagement.Application.Features.YearlyReturns.Request.Queries;

namespace SchoolManagement.Api.Controllers
{

    [Route(SMSRoutePrefix.YearlyReturn)]
    [ApiController]
    [Authorize]
    public class YearlyReturnController:ControllerBase
    {
        private readonly IMediator _mediator;

        public YearlyReturnController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-YearlyReturn")]
        public async Task<ActionResult<List<YearlyReturnDto>>> Get([FromQuery] QueryParams queryParams)
        {
            var YearlyReturns = await _mediator.Send(new GetYearlyReturnListRequest { QueryParams = queryParams });
            return Ok(YearlyReturns);
        }

        [HttpGet]
        [Route("get-YearlyReturnDetail/{id}")]
        public async Task<ActionResult<YearlyReturnDto>> Get(int id)
        {
            var YearlyReturns = await _mediator.Send(new GetYearlyReturnDetailRequest { YearlyReturnId = id });
            return Ok(YearlyReturns);
        }



        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-YearlyReturn")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateYearlyReturnDto YearlyReturn)
        {
            var command = new CreateYearlyReturnCommand { YearlyReturnDto = YearlyReturn };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-YearlyReturn/{id}")]
        public async Task<ActionResult> Put([FromForm] YearlyReturnDto yearlyReturn)
        {
            var command = new UpdateYearlyReturnCommand { YearlyReturnDto = yearlyReturn };
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("delete-YearlyReturn/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteYearlyReturnCommand { YearlyReturnId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

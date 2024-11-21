using SchoolManagement.Application.DTOs.MonthlyReturns;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands;
using SchoolManagement.Application.Features.YearlyReturns.Request.Commands;

namespace SchoolManagement.Api.Controllers
{
    public class YearlyReturnController:ControllerBase
    {
        private readonly IMediator _mediator;

        public YearlyReturnController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}

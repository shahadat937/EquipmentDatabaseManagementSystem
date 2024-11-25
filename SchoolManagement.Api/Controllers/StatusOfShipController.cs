using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.StatusOfShip;
using SchoolManagement.Application.DTOs.YearlyReturns;
using SchoolManagement.Application.Features.StatusOfShip.Request.Commands;
using SchoolManagement.Application.Features.StatusOfShip.Request.Queries;
using SchoolManagement.Application.Features.YearlyReturns.Request.Commands;
using SchoolManagement.Application.Features.YearlyReturns.Request.Queries;

namespace SchoolManagement.Api.Controllers
{

    [Route(SMSRoutePrefix.StatusOfShip)]
    [ApiController]
    [Authorize]
    public class StatusOfShipController:ControllerBase
    {
        private readonly IMediator _mediator;

        public StatusOfShipController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("get-StatusOfShips")]
        public async Task<ActionResult<List<StatusOfShipDto>>> Get([FromQuery] QueryParams queryParams)
        {
            var StatusOfShips = await _mediator.Send(new GetStatusOfShipListRequest { QueryParams = queryParams });
            return Ok(StatusOfShips);
        }

        [HttpGet]
        [Route("get-StatusOfShipsDetail/{id}")]
        public async Task<ActionResult<StatusOfShipDto>> Get(int id)
        {
            var StatusOfShips = await _mediator.Send(new GetStatusOfShipDetailsRequest{ StatusOfShipId = id });
            return Ok(StatusOfShips);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-StatusOfShip")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateStatusOfShipDto StatusOfShip)
        {
            var command = new CreateStatusOfShipCommand { StatusOfShipDto = StatusOfShip };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-StatusOfShips/{id}")]
        public async Task<ActionResult> Put([FromForm] StatusOfShipDto statusOfShip)
        {
            var command = new UpdateStatusOfShipCommand { StatusOfShipDto = statusOfShip };
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("delete-StatusOfShips/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteStatusOfShipCommand { StatusOfShipId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

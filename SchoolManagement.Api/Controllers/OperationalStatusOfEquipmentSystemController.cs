using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem;
using SchoolManagement.Application.Features.AppFeature.Requests.Commands;
using SchoolManagement.Application.Features.AppFeature.Requests.Queries;
using SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Handlers.Commands;
using SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Commands;
using SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Queries;

namespace SchoolManagement.Api.Controllers
{
    [Route(SMSRoutePrefix.OperationalStatusOfEquipmentSystem)]
    [ApiController]
    [Authorize]
    public class OperationalStatusOfEquipmentSystemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperationalStatusOfEquipmentSystemController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-operationalStatusOfEquipmentSystem")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateOperationalStatusOfEquipmentSystemDto operationalStatus)
        {
            var command = new CreateOperationalStatusOfEquipmentSystemCommand { 
            OperationalStatusOfEquipmentSystemDto = operationalStatus
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-operationalStatusOfEquipmentSystem/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateOperationalStatusOfEquipmentSystemDto operationalStatus)
        {
            var command = new UpdateOperationalStatusOfEquipmentSystemCommand { 
                OperationalStatusOfEquipmentSystemDto = operationalStatus
            };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("delete-operationalStatusOfEquipmentSystem/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOperationalStatusOfEquipmentSystemCommand { OperationalStatusOfEquipmentSystemId = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        [Route("get-operationalStatusOfEquipmentSystem")]
        public async Task<ActionResult<List<OperationalStatusOfEquipmentSystemDto>>> Get([FromQuery] QueryParams queryParams)
        {
            var result = await _mediator.Send(new GetOperationalStatusOfEquipmentSystemListCommand { QueryParams = queryParams });
            return Ok(result);
        }

        [HttpGet]
        [Route("get-operationalStatusOfEquipmentSystem/{id}")]
        public async Task<ActionResult<FeatureDto>> Get(int id)
        {
            var Feature = await _mediator.Send(new GetOperationalStatusOfEquipmentSystemDetailsCommand { OperationalStatusOfEquipmentSystemId = id });
            return Ok(Feature);
        }
    }
}

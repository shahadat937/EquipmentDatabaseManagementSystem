using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Envelope;
using SchoolManagement.Application.Features.Envelopes.Requests.Commands;
using SchoolManagement.Application.Features.Envelopes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Envelope)]
[ApiController]
[Authorize]
public class EnvelopeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnvelopeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Envelopes")]
    public async Task<ActionResult<List<EnvelopeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Envelopes = await _mediator.Send(new GetEnvelopeListRequest { QueryParams = queryParams });
        return Ok(Envelopes);
    }


    [HttpGet]
    [Route("get-EnvelopeDetail/{id}")]
    public async Task<ActionResult<EnvelopeDto>> Get(int id)
    {
        var Envelope = await _mediator.Send(new GetEnvelopeDetailRequest { EnvelopeId = id });
        return Ok(Envelope);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Envelope")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEnvelopeDto Envelope)
    {
        var command = new CreateEnvelopeCommand { EnvelopeDto = Envelope };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Envelope/{id}")]
    public async Task<ActionResult> Put([FromBody] EnvelopeDto Envelope)
    {
        var command = new UpdateEnvelopeCommand { EnvelopeDto = Envelope };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Envelope/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteEnvelopeCommand { EnvelopeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedEnvelope")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedEnvelope()
    {
        var Envelope = await _mediator.Send(new GetSelectedEnvelopeRequest { });
        return Ok(Envelope);
    }
}


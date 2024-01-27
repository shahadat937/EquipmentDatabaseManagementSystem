using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;
using SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Commands;
using SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.BookUserManualBRInfo)]
[ApiController]
[Authorize]
public class BookUserManualBRInfoController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookUserManualBRInfoController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-BookUserManualBRInfos")]
    public async Task<ActionResult<List<BookUserManualBRInfoDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var BookUserManualBRInfos = await _mediator.Send(new GetBookUserManualBRInfoListRequest { QueryParams = queryParams });
        return Ok(BookUserManualBRInfos);
    }


    [HttpGet]
    [Route("get-BookUserManualBRInfoDetail/{id}")]
    public async Task<ActionResult<BookUserManualBRInfoDto>> Get(int id)
    {
        var BookUserManualBRInfo = await _mediator.Send(new GetBookUserManualBRInfoDetailRequest { BookUserManualBRInfoId = id });
        return Ok(BookUserManualBRInfo);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-BookUserManualBRInfo")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateBookUserManualBRInfoDto BookUserManualBRInfo)
    {
        var command = new CreateBookUserManualBRInfoCommand { BookUserManualBRInfoDto = BookUserManualBRInfo };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-BookUserManualBRInfo/{id}")]
    public async Task<ActionResult> Put([FromBody] BookUserManualBRInfoDto BookUserManualBRInfo)
    {
        var command = new UpdateBookUserManualBRInfoCommand { BookUserManualBRInfoDto = BookUserManualBRInfo };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-BookUserManualBRInfo/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteBookUserManualBRInfoCommand { BookUserManualBRInfoId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedBookUserManualBRInfo")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedBookUserManualBRInfo()
    {
        var BookUserManualBRInfo = await _mediator.Send(new GetSelectedBookUserManualBRInfoRequest { });
        return Ok(BookUserManualBRInfo);
    }
}


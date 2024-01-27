using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.BookType;
using SchoolManagement.Application.Features.BookTypes.Requests.Commands;
using SchoolManagement.Application.Features.BookTypes.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.BookType)]
[ApiController]
[Authorize]
public class BookTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-BookTypes")]
    public async Task<ActionResult<List<BookTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var BookTypes = await _mediator.Send(new GetBookTypeListRequest { QueryParams = queryParams });
        return Ok(BookTypes);
    }


    [HttpGet]
    [Route("get-BookTypeDetail/{id}")]
    public async Task<ActionResult<BookTypeDto>> Get(int id)
    {
        var BookType = await _mediator.Send(new GetBookTypeDetailRequest { BookTypeId = id });
        return Ok(BookType);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-BookType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBookTypeDto BookType)
    {
        var command = new CreateBookTypeCommand { BookTypeDto = BookType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-BookType/{id}")]
    public async Task<ActionResult> Put([FromBody] BookTypeDto BookType)
    {
        var command = new UpdateBookTypeCommand { BookTypeDto = BookType };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-BookType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteBookTypeCommand { BookTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedBookType")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedBookType()
    {
        var BookType = await _mediator.Send(new GetSelectedBookTypeRequest { });
        return Ok(BookType);
    }
}


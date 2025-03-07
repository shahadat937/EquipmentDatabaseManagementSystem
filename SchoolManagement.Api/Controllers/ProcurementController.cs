﻿using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Commands;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Procurement)]
[ApiController]
[Authorize]
public class ProcurementController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProcurementController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("get-Procurements")]
    public async Task<ActionResult<List<ProcurementDto>>> Get([FromQuery] QueryParams queryParams, string orderBy, string orderDirection)
    {
        var Procurements = await _mediator.Send(new GetProcurementListRequest {
            QueryParams = queryParams,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        }

        );
        return Ok(Procurements);
    }


    [HttpGet]
    [Route("get-Procurements-by-procurementMethodId/{procurementMethodId}")]
    public async Task<ActionResult<List<ProcurementDto>>> GetProcurementListByProcurementMethodId([FromQuery] QueryParams queryParams,  int procurementMethodId)
    {
        var Procurements = await _mediator.Send(new GetProcurementListByProcurementMethodIdRequest
        {
            QueryParams = queryParams,
            ProcureMethodId = procurementMethodId
        }

        );
        return Ok(Procurements);
    }
    
    [HttpGet]
    [Route("get-Procurements-by-procurementMethodId-authorityId/{procurementMethodId}/{authorityId}")]
    public async Task<ActionResult<List<ProcurementDto>>> GetProcurementListByProcurementMethodIdAndAuthorityId([FromQuery] QueryParams queryParams, string searchBy, int procurementMethodId, int authorityId)
    {
        var Procurements = await _mediator.Send(new GetProcurementListByProcurementMethodIdAndAuthorityIdRequest
        {
            QueryParams = queryParams,
            SearchBy = searchBy,
            ProcureMethodId = procurementMethodId,
            AuthorityId = authorityId
        }

        );
        return Ok(Procurements);
    }





    [HttpGet]
    [Route("get-ProcurementDetail/{id}")]
    public async Task<ActionResult<ProcurementDto>> Get(int id)
    {
        var Procurement = await _mediator.Send(new GetProcurementDetailRequest { ProcurementId = id });
        return Ok(Procurement);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Procurement")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProcurementDto Procurement)
    {
        var command = new CreateProcurementCommand { ProcurementDto = Procurement };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Procurement/{id}")]
    public async Task<ActionResult> Put([FromBody] CreateProcurementDto procurement)
    {
        var command = new UpdateProcurementCommand { ProcurementDto = procurement };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Procurement/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProcurementCommand { ProcurementId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedProcurement")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedProcurement()
    {
        var Procurement = await _mediator.Send(new GetSelectedProcurementRequest { });
        return Ok(Procurement);
    }


    [HttpGet]
    [Route("get-AIP-Pending-Procurements")]
    public async Task<ActionResult<List<ProcurementDto>>> GetAipPendingProcurements([FromQuery] QueryParams queryParams, string orderBy, string orderDirection)
    {
        var Procurements = await _mediator.Send(new GetAipPendingProcurementsListRequest
        {
            QueryParams = queryParams,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        }

        );
        return Ok(Procurements);
    }
    
    [HttpGet]
    [Route("get-Ongoing-Tender-Spec-Preparation-Procurements")]
    public async Task<ActionResult<List<ProcurementDto>>> GetOngoingTenderSpecPreparationProcurements([FromQuery] QueryParams queryParams, string orderBy, string orderDirection)
    {
        var Procurements = await _mediator.Send(new GetOngoingTenderSpecPreparationProcurementsListRequest
        {
            QueryParams = queryParams,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        }

        );
        return Ok(Procurements);
    }


    [HttpGet]
    [Route("get-Tender-Floated-Procurements")]
    public async Task<ActionResult<List<ProcurementDto>>> GetTenderFloatedProcurements([FromQuery] QueryParams queryParams, string orderBy, string orderDirection)
    {
        var Procurements = await _mediator.Send(new GetTenderFloatedProcurementsListRequest
        {
            QueryParams = queryParams,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        }

        );
        return Ok(Procurements);
    }
    
    [HttpGet]
    [Route("get-Offer-Under-Evaluation-Procurements")]
    public async Task<ActionResult<List<ProcurementDto>>> GetOfferUnderEvaluationProcurements([FromQuery] QueryParams queryParams, string orderBy, string orderDirection)
    {
        var Procurements = await _mediator.Send(new GetOfferUnderEvaluationProcurementsListRequest
        {
            QueryParams = queryParams,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        }

        );
        return Ok(Procurements);
    }
    
    [HttpGet]
    [Route("get-Tender-Opening-Procurements")]
    public async Task<ActionResult<List<ProcurementDto>>> GetTenderOpeningProcurements([FromQuery] QueryParams queryParams, string orderBy, string orderDirection)
    {
        var Procurements = await _mediator.Send(new GetTenderOpeningProcurementsListRequest
        {
            QueryParams = queryParams,
            OrderBy = orderBy,
            OrderDirection = orderDirection
        }

        );
        return Ok(Procurements);
    }


}


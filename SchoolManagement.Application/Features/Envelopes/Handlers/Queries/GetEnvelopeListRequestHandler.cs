using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Envelope;
using SchoolManagement.Application.Features.Envelopes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Envelopes.Handlers.Queries
{
    public class GetEnvelopeListRequestHandler : IRequestHandler<GetEnvelopeListRequest, PagedResult<EnvelopeDto>>
    {

        private readonly ISchoolManagementRepository<Envelope> _EnvelopeRepository;

        private readonly IMapper _mapper;

        public GetEnvelopeListRequestHandler(ISchoolManagementRepository<Envelope> EnvelopeRepository, IMapper mapper)
        {
            _EnvelopeRepository = EnvelopeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EnvelopeDto>> Handle(GetEnvelopeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Envelope> Envelopes = _EnvelopeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Envelopes.Count();
            Envelopes = Envelopes.OrderByDescending(x => x.EnvelopeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var EnvelopeDtos = _mapper.Map<List<EnvelopeDto>>(Envelopes);
            var result = new PagedResult<EnvelopeDto>(EnvelopeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

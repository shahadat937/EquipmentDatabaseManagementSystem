using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetProcurementListRequestHandler : IRequestHandler<GetProcurementListRequest, PagedResult<ProcurementDto>>
    {

        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;

        private readonly IMapper _mapper;

        public GetProcurementListRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository, IMapper mapper)
        {
            _ProcurementRepository = ProcurementRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProcurementDto>> Handle(GetProcurementListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Procurement> Procurements = _ProcurementRepository.FilterWithInclude(x => (x.AIP.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Procurements.Count();
            Procurements = Procurements.OrderByDescending(x => x.ProcurementId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ProcurementDtos = _mapper.Map<List<ProcurementDto>>(Procurements);
            var result = new PagedResult<ProcurementDto>(ProcurementDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

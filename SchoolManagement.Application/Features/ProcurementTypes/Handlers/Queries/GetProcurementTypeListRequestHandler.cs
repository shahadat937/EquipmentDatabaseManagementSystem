using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ProcurementType;
using SchoolManagement.Application.Features.ProcurementTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProcurementTypes.Handlers.Queries
{
    public class GetProcurementTypeListRequestHandler : IRequestHandler<GetProcurementTypeListRequest, PagedResult<ProcurementTypeDto>>
    {

        private readonly ISchoolManagementRepository<ProcurementType> _ProcurementTypeRepository;

        private readonly IMapper _mapper;

        public GetProcurementTypeListRequestHandler(ISchoolManagementRepository<ProcurementType> ProcurementTypeRepository, IMapper mapper)
        {
            _ProcurementTypeRepository = ProcurementTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProcurementTypeDto>> Handle(GetProcurementTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ProcurementType> ProcurementTypes = _ProcurementTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ProcurementTypes.Count();
            ProcurementTypes = ProcurementTypes.OrderByDescending(x => x.ProcurementTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ProcurementTypeDtos = _mapper.Map<List<ProcurementTypeDto>>(ProcurementTypes);
            var result = new PagedResult<ProcurementTypeDto>(ProcurementTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

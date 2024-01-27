using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ProcurementMethod;
using SchoolManagement.Application.Features.ProcurementMethods.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProcurementMethods.Handlers.Queries
{
    public class GetProcurementMethodListRequestHandler : IRequestHandler<GetProcurementMethodListRequest, PagedResult<ProcurementMethodDto>>
    {

        private readonly ISchoolManagementRepository<ProcurementMethod> _ProcurementMethodRepository;

        private readonly IMapper _mapper;

        public GetProcurementMethodListRequestHandler(ISchoolManagementRepository<ProcurementMethod> ProcurementMethodRepository, IMapper mapper)
        {
            _ProcurementMethodRepository = ProcurementMethodRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProcurementMethodDto>> Handle(GetProcurementMethodListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ProcurementMethod> ProcurementMethods = _ProcurementMethodRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ProcurementMethods.Count();
            ProcurementMethods = ProcurementMethods.OrderByDescending(x => x.ProcurementMethodId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ProcurementMethodDtos = _mapper.Map<List<ProcurementMethodDto>>(ProcurementMethods);
            var result = new PagedResult<ProcurementMethodDto>(ProcurementMethodDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

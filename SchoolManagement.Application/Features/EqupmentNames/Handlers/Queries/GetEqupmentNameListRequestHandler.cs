using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.EqupmentNames;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EqupmentNames.Handlers.Queries
{
    public class GetEqupmentNameListRequestHandler : IRequestHandler<GetEqupmentNameListRequest, PagedResult<EqupmentNameDto>>
    {

        private readonly ISchoolManagementRepository<EqupmentName> _EqupmentNameRepository;

        private readonly IMapper _mapper;

        public GetEqupmentNameListRequestHandler(ISchoolManagementRepository<EqupmentName> EqupmentNameRepository, IMapper mapper)
        {
            _EqupmentNameRepository = EqupmentNameRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EqupmentNameDto>> Handle(GetEqupmentNameListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<EqupmentName> EqupmentNames = _EqupmentNameRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "EquipmentCategory");
            var totalCount = EqupmentNames.Count();
            EqupmentNames = EqupmentNames.OrderByDescending(x => x.EqupmentNameId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var EqupmentNameDtos = _mapper.Map<List<EqupmentNameDto>>(EqupmentNames);
            var result = new PagedResult<EqupmentNameDto>(EqupmentNameDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes;
using SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Handlers.Queries
{
    public class GetQuarterlyReturnNoTypeListRequestHandler : IRequestHandler<GetQuarterlyReturnNoTypeListRequest, PagedResult<QuarterlyReturnNoTypeDto>>
    {

        private readonly ISchoolManagementRepository<QuarterlyReturnNoType> _QuarterlyReturnNoTypeRepository;

        private readonly IMapper _mapper;

        public GetQuarterlyReturnNoTypeListRequestHandler(ISchoolManagementRepository<QuarterlyReturnNoType> QuarterlyReturnNoTypeRepository, IMapper mapper)
        {
            _QuarterlyReturnNoTypeRepository = QuarterlyReturnNoTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<QuarterlyReturnNoTypeDto>> Handle(GetQuarterlyReturnNoTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<QuarterlyReturnNoType> QuarterlyReturnNoTypes = _QuarterlyReturnNoTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = QuarterlyReturnNoTypes.Count();
            QuarterlyReturnNoTypes = QuarterlyReturnNoTypes.OrderByDescending(x => x.QuarterlyReturnNoTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var QuarterlyReturnNoTypeDtos = _mapper.Map<List<QuarterlyReturnNoTypeDto>>(QuarterlyReturnNoTypes);
            var result = new PagedResult<QuarterlyReturnNoTypeDto>(QuarterlyReturnNoTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

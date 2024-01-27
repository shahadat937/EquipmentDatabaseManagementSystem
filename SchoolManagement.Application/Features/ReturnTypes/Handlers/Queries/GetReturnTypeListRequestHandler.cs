using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ReturnTypes;
using SchoolManagement.Application.Features.ReturnTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReturnTypes.Handlers.Queries
{
    public class GetReturnTypeListRequestHandler : IRequestHandler<GetReturnTypeListRequest, PagedResult<ReturnTypeDto>>
    {

        private readonly ISchoolManagementRepository<ReturnType> _ReturnTypeRepository;

        private readonly IMapper _mapper;

        public GetReturnTypeListRequestHandler(ISchoolManagementRepository<ReturnType> ReturnTypeRepository, IMapper mapper)
        {
            _ReturnTypeRepository = ReturnTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReturnTypeDto>> Handle(GetReturnTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ReturnType> ReturnTypes = _ReturnTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ReturnTypes.Count();
            ReturnTypes = ReturnTypes.OrderByDescending(x => x.ReturnTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ReturnTypeDtos = _mapper.Map<List<ReturnTypeDto>>(ReturnTypes);
            var result = new PagedResult<ReturnTypeDto>(ReturnTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

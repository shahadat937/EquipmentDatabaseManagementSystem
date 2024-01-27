using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.FcLc;
using SchoolManagement.Application.Features.FcLcs.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FcLcs.Handlers.Queries
{
    public class GetFcLcListRequestHandler : IRequestHandler<GetFcLcListRequest, PagedResult<FcLcDto>>
    {

        private readonly ISchoolManagementRepository<FcLc> _FcLcRepository;

        private readonly IMapper _mapper;

        public GetFcLcListRequestHandler(ISchoolManagementRepository<FcLc> FcLcRepository, IMapper mapper)
        {
            _FcLcRepository = FcLcRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<FcLcDto>> Handle(GetFcLcListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<FcLc> FcLcs = _FcLcRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = FcLcs.Count();
            FcLcs = FcLcs.OrderByDescending(x => x.FcLcId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var FcLcDtos = _mapper.Map<List<FcLcDto>>(FcLcs);
            var result = new PagedResult<FcLcDto>(FcLcDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

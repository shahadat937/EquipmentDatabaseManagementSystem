using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Priority;
using SchoolManagement.Application.Features.Prioritys.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Prioritys.Handlers.Queries
{
    public class GetPriorityListRequestHandler : IRequestHandler<GetPriorityListRequest, PagedResult<PriorityDto>>
    {

        private readonly ISchoolManagementRepository<Priority> _PriorityRepository;

        private readonly IMapper _mapper;

        public GetPriorityListRequestHandler(ISchoolManagementRepository<Priority> PriorityRepository, IMapper mapper)
        {
            _PriorityRepository = PriorityRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PriorityDto>> Handle(GetPriorityListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Priority> Prioritys = _PriorityRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Prioritys.Count();
            Prioritys = Prioritys.OrderByDescending(x => x.PriorityId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var PriorityDtos = _mapper.Map<List<PriorityDto>>(Prioritys);
            var result = new PagedResult<PriorityDto>(PriorityDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

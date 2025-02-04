using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.DailyWorkState;
using SchoolManagement.Application.Features.DailyWorkStates.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyWorkStates.Handlers.Queries
{
    public class GetDailyWorkStateListRequestHandler : IRequestHandler<GetDailyWorkStateListRequest, PagedResult<DailyWorkStateDto>>
    {

        private readonly ISchoolManagementRepository<DailyWorkState> _DailyWorkStateRepository;

        private readonly IMapper _mapper;

        public GetDailyWorkStateListRequestHandler(ISchoolManagementRepository<DailyWorkState> DailyWorkStateRepository, IMapper mapper)
        {
            _DailyWorkStateRepository = DailyWorkStateRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<DailyWorkStateDto>> Handle(GetDailyWorkStateListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<DailyWorkState> DailyWorkStates = _DailyWorkStateRepository.FilterWithInclude(x => x.ActionTaken.Name == request.ActionTaken && (x.Subject.Contains(request.QueryParams.SearchText) ||(x.LetterType.Name.Contains(request.QueryParams.SearchText)) ||String.IsNullOrEmpty(request.QueryParams.SearchText)), "LetterType", "DealingOfficer", "ActionTaken", "Priority");
            var totalCount = DailyWorkStates.Count();
            DailyWorkStates = DailyWorkStates.Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var DailyWorkStateDtos = _mapper.Map<List<DailyWorkStateDto>>(DailyWorkStates);
            var result = new PagedResult<DailyWorkStateDto>(DailyWorkStateDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ActionTaken;
using SchoolManagement.Application.Features.ActionTakens.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ActionTakens.Handlers.Queries
{
    public class GetActionTakenListRequestHandler : IRequestHandler<GetActionTakenListRequest, PagedResult<ActionTakenDto>>
    {

        private readonly ISchoolManagementRepository<ActionTaken> _ActionTakenRepository;

        private readonly IMapper _mapper;

        public GetActionTakenListRequestHandler(ISchoolManagementRepository<ActionTaken> ActionTakenRepository, IMapper mapper)
        {
            _ActionTakenRepository = ActionTakenRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ActionTakenDto>> Handle(GetActionTakenListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ActionTaken> ActionTakens = _ActionTakenRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ActionTakens.Count();
            ActionTakens = ActionTakens.OrderByDescending(x => x.ActionTakenId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ActionTakenDtos = _mapper.Map<List<ActionTakenDto>>(ActionTakens);
            var result = new PagedResult<ActionTakenDto>(ActionTakenDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

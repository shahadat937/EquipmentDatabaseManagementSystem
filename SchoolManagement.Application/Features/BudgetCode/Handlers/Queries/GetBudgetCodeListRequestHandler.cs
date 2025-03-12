using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Domain;

using SchoolManagement.Application.Features.BudgetCode.Requests.Queries;
using SchoolManagement.Application.DTOs.BudgetCode;

namespace SchoolManagement.Application.Features.ReporingYears.Handlers.Queries
{
    public class GetBudgetCodeListRequestHandler : IRequestHandler<GetBudgetCodeListRequest, PagedResult<BudgetCodeDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.BudgetCode> _ReporingYearRepository;

        private readonly IMapper _mapper;

        public GetBudgetCodeListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.BudgetCode> ReporingYearRepository, IMapper mapper)
        {
            _ReporingYearRepository = ReporingYearRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BudgetCodeDto>> Handle(GetBudgetCodeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.BudgetCode> ReporingYears = _ReporingYearRepository.FilterWithInclude(x => (x.BudgetCodeName.ToString().Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ReporingYears.Count();
            ReporingYears = ReporingYears.OrderByDescending(x => x.BudgetCodeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ReporingYearDtos = _mapper.Map<List<BudgetCodeDto>>(ReporingYears);
            var result = new PagedResult<BudgetCodeDto>(ReporingYearDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Budget;
using SchoolManagement.Application.Features.Budget.Requests.Queries;
using SchoolManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Application.Features.Budget.Handlers.Queries
{
    public class GetLeatestFinanciYearBugetRequestHandler : IRequestHandler<GetLeatestFinanciYearBugetRequest, PagedResult<BudgetDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Budget> _BudgetRepository;
        private readonly ISchoolManagementRepository<BudgetBaseSchoolName> _BudgetBaseSchoolNameRepository;
        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;


        private readonly IMapper _mapper;

        public GetLeatestFinanciYearBugetRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Budget> BudgetRepository, IMapper mapper, ISchoolManagementRepository<BudgetBaseSchoolName> BudgetBaseSchoolNameRepository, ISchoolManagementRepository<BaseSchoolName> baseSchoolNameRepository)
        {
            _BudgetRepository = BudgetRepository;
            _mapper = mapper;
            _BudgetBaseSchoolNameRepository = BudgetBaseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
        }

        public async Task<PagedResult<BudgetDto>> Handle(GetLeatestFinanciYearBugetRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.Budget> BudgetQuery;

            var latestFinancialYear = await _BudgetRepository
                                      .FilterWithInclude(x => true)
                                      .Select(x => x.FinancialYear.FinancialYearName)
                                      .Distinct()
                                      .OrderByDescending(x => x)
                                      .FirstOrDefaultAsync();

            BudgetQuery = _BudgetRepository.FilterWithInclude(
          x => x.FinancialYear.FinancialYearName == latestFinancialYear && x.BudgetCode.Code == request.BudgetCode && 
               (string.IsNullOrEmpty(request.QueryParams.SearchText) ||
               x.FinancialYear.FinancialYearName.Contains(request.QueryParams.SearchText) ||
               x.DgdpNssd.Name.Contains(request.QueryParams.SearchText) ||
               x.Controlled.Name.Contains(request.QueryParams.SearchText) ||
               x.EqupmentName.Name.Contains(request.QueryParams.SearchText) ||
               x.Remarks.Contains(request.QueryParams.SearchText) ||
               x.BudgetBaseSchoolNames.Any(b => b.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText))),
           "EqupmentName", "BudgetCode", "Controlled", "DgdpNssd", "FinancialYear", "BudgetBaseSchoolNames.BaseSchoolName"
      );


            // Apply custom ordering
            var orderedQuery = BudgetQuery;

            if (request.OrderBy != null)
            {
                switch (request.OrderBy.ToLower())
                {
                    case "equpmentname":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.EqupmentName.Name)
                            : orderedQuery.OrderBy(x => x.EqupmentName.Name);
                        break;
                

                    case "financialyear":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.FinancialYear.FinancialYearName)
                            : orderedQuery.OrderBy(x => x.FinancialYear.FinancialYearName);
                        break;

                    case "agency":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.DgdpNssd.Name)
                            : orderedQuery.OrderBy(x => x.DgdpNssd.Name);
                        break;

                    case "controlled":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.Controlled.Name)
                            : orderedQuery.OrderBy(x => x.Controlled.Name);
                        break;

                    case "baseschoolname":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.BudgetBaseSchoolNames
                                .OrderByDescending(b => b.BaseSchoolName.SchoolName)
                                .Select(b => b.BaseSchoolName.SchoolName)
                                .FirstOrDefault())
                            : orderedQuery.OrderBy(x => x.BudgetBaseSchoolNames
                                .OrderBy(b => b.BaseSchoolName.SchoolName)
                                .Select(b => b.BaseSchoolName.SchoolName)
                                .FirstOrDefault());
                        break;

                    case "Budgetid":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                           ? orderedQuery.OrderByDescending(x => x.BudgetId)
                           : orderedQuery.OrderBy(x => x.BudgetId);
                        break;
                    case "qty":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                           ? orderedQuery.OrderByDescending(x => x.Qty)
                           : orderedQuery.OrderBy(x => x.Qty);
                        break;
                    case "budgetcode":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                           ? orderedQuery.OrderByDescending(x => x.BudgetCode)
                           : orderedQuery.OrderBy(x => x.BudgetCode);
                        break;
       

                    case "remarks":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                           ? orderedQuery.OrderByDescending(x => x.BudgetCode)
                           : orderedQuery.OrderBy(x => x.BudgetCode);
                        break;



                    default:
                        orderedQuery = orderedQuery.OrderByDescending(x => x.BudgetId);
                        break;
                }

            }
            else
            {
                // Default ordering
                orderedQuery = orderedQuery.OrderByDescending(x => x.BudgetId);
            }

            var totalCount = await orderedQuery.CountAsync();

            var Budget = await orderedQuery
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToListAsync();

            var baseSchoolNames = await _BudgetBaseSchoolNameRepository.GetAll();
           
            var allBaseSchool = await _BaseSchoolNameRepository.GetAll();

            var BudgetDtos = _mapper.Map<List<BudgetDto>>(Budget);

            foreach (var BudgetDto in BudgetDtos)
            {
                BudgetDto.BaseSchoolNameDtos = baseSchoolNames
                    .Where(x => x.BudgetId == BudgetDto.BudgetId)
                    .Select(x => new BudgetBaseSchoolNameDto
                    {
                        BaseSchoolNameId = x.BaseSchoolNameId,
                        BaseSchoolName = allBaseSchool.FirstOrDefault(b => b.BaseSchoolNameId == x.BaseSchoolNameId)?.SchoolName
                    }).ToList();

              
            }

            var result = new PagedResult<BudgetDto>(
                BudgetDtos,
                totalCount,
                request.QueryParams.PageNumber,
                request.QueryParams.PageSize
            );

            return result;
        }

    



    }
}
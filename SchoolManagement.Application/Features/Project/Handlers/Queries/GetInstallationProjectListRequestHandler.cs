using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Project;
using SchoolManagement.Application.Features.Project.Requests.Queries;
using SchoolManagement.Domain;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Helpers;
using System.Linq.Expressions;

namespace SchoolManagement.Application.Features.Project.Handlers.Queries
{
    public class GetInstallationProjectListRequestHandler : IRequestHandler<GetInstallationProjectListRequest, PagedResult<ProjectDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Project> _ProjectRepository;
        private readonly ISchoolManagementRepository<ProjectBaseSchoolName> _ProjectBaseSchoolNameRepository;
        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;


        private readonly IMapper _mapper;

        public GetInstallationProjectListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Project> ProjectRepository, IMapper mapper, ISchoolManagementRepository<ProjectBaseSchoolName> ProjectBaseSchoolNameRepository, ISchoolManagementRepository<BaseSchoolName> baseSchoolNameRepository)
        {
            _ProjectRepository = ProjectRepository;
            _mapper = mapper;
            _ProjectBaseSchoolNameRepository = ProjectBaseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
        }

        public async Task<PagedResult<ProjectDto>> Handle(GetInstallationProjectListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.Project> ProjectQuery;

            DateTime dateSearch;
            bool searchDate = DateTime.TryParse(request.QueryParams.SearchText, out dateSearch);

            ProjectQuery = _ProjectRepository.FilterWithInclude(
                x => (x.ShipmentAndDelivery!=null || x.SAT !=null || x.HAT != null || x.SAT !=null  || x.LocalTraining !=null || x.FinalAcceptance != null)
                &&
                    
                    // Search 
                    (string.IsNullOrEmpty(request.QueryParams.SearchText) ||                    
                     x.FinancialYear.FinancialYearName.Contains(request.QueryParams.SearchText) ||
                     x.DgdpNssd.Name.Contains(request.QueryParams.SearchText) ||
                     x.Controlled.Name.Contains(request.QueryParams.SearchText) ||
                     x.EqupmentName.Name.Contains(request.QueryParams.SearchText) ||
                     x.FinancialYear.FinancialYearName.Contains(request.QueryParams.SearchText) ||
                     x.EPrice.ToString().Contains(request.QueryParams.SearchText) ||
                     x.FatRemarks.Contains(request.QueryParams.SearchText) ||
                     x.InstallationRemarks.Contains(request.QueryParams.SearchText) ||
                     x.ForeignTrainingRemark.Contains(request.QueryParams.SearchText) ||                  
                     x.BudgetCode.BudgetCodeName.Contains(request.QueryParams.SearchText) || 
                     x.BudgetCode.Code.Contains(request.QueryParams.SearchText) 
                     || x.ProjectBaseSchoolName.Any(b => b.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText))),
                 "EqupmentName", "GroupName", "FcLc", "BudgetCode", "Controlled",  "DgdpNssd", "FinancialYear",  "ProjectBaseSchoolName.BaseSchoolName"
            );

            // Apply custom ordering
            var orderedQuery = ProjectQuery;

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
                            ? orderedQuery.OrderByDescending(x => x.ProjectBaseSchoolName
                                .OrderByDescending(b => b.BaseSchoolName.SchoolName)
                                .Select(b => b.BaseSchoolName.SchoolName)
                                .FirstOrDefault())
                            : orderedQuery.OrderBy(x => x.ProjectBaseSchoolName
                                .OrderBy(b => b.BaseSchoolName.SchoolName)
                                .Select(b => b.BaseSchoolName.SchoolName)
                                .FirstOrDefault());
                        break;

                    case "Projectid":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                           ? orderedQuery.OrderByDescending(x => x.ProjectId)
                           : orderedQuery.OrderBy(x => x.ProjectId);
                        break;
                    case "qty":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                           ? orderedQuery.OrderByDescending(x => x.Qty)
                           : orderedQuery.OrderBy(x => x.Qty);
                        break;

                    default:
                        orderedQuery = orderedQuery.OrderByDescending(x => x.LastModifiedDate);
                        break;
                }

            }
            else
            {
                // Default ordering
                orderedQuery = orderedQuery.OrderByDescending(x => x.LastModifiedDate);
            }

            var totalCount = await orderedQuery.CountAsync();

            var Project = await orderedQuery
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToListAsync();

            var baseSchoolNames = await _ProjectBaseSchoolNameRepository.GetAll();
           
            var allBaseSchool = await _BaseSchoolNameRepository.GetAll();

            var ProjectDtos = _mapper.Map<List<ProjectDto>>(Project);

            foreach (var ProjectDto in ProjectDtos)
            {
                ProjectDto.BaseSchoolNameDtos = baseSchoolNames
                    .Where(x => x.ProjectId == ProjectDto.ProjectId)
                    .Select(x => new ProjectBaseSchoolNameDto
                    {
                        BaseSchoolNameId = x.BaseSchoolNameId,
                        BaseSchoolName = allBaseSchool.FirstOrDefault(b => b.BaseSchoolNameId == x.BaseSchoolNameId)?.SchoolName
                    }).ToList();

              
            }

            var result = new PagedResult<ProjectDto>(
                ProjectDtos,
                totalCount,
                request.QueryParams.PageNumber,
                request.QueryParams.PageSize
            );

            return result;
        }


    }
}
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Helpers;
using System.Linq.Expressions;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetProcurementListRequestHandler : IRequestHandler<GetProcurementListRequest, PagedResult<ProcurementDto>>
    {

        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;
        private readonly ISchoolManagementRepository<ProcurementBaseSchoolName> _ProcurementBaseSchoolNameRepository;
        private readonly ISchoolManagementRepository<ProcurementTenderOpening> _ProcurementTenderOpeningRepository;
        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;


        private readonly IMapper _mapper;

        public GetProcurementListRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository, IMapper mapper, ISchoolManagementRepository<ProcurementBaseSchoolName> ProcurementBaseSchoolNameRepository, ISchoolManagementRepository<ProcurementTenderOpening> ProcurementTenderOpeningRepository, ISchoolManagementRepository<BaseSchoolName> baseSchoolNameRepository)
        {
            _ProcurementRepository = ProcurementRepository;
            _mapper = mapper;
            _ProcurementTenderOpeningRepository = ProcurementTenderOpeningRepository;
            _ProcurementBaseSchoolNameRepository = ProcurementBaseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
            _BaseSchoolNameRepository = baseSchoolNameRepository;
        }

        public async Task<PagedResult<ProcurementDto>> Handle(GetProcurementListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Procurement> procurementsQuery;

            DateTime dateSearch;
            bool searchDate = DateTime.TryParse(request.QueryParams.SearchText, out dateSearch);

            procurementsQuery = _ProcurementRepository.FilterWithInclude(
                x => string.IsNullOrEmpty(request.QueryParams.SearchText) ||
                     x.FcLc.Name.Contains(request.QueryParams.SearchText) ||
                     x.GroupName.Name.Contains(request.QueryParams.SearchText) ||
                     x.FinancialYear.FinancialYearName.Contains(request.QueryParams.SearchText) ||
                     x.DgdpNssd.Name.Contains(request.QueryParams.SearchText) ||
                     x.Controlled.Name.Contains(request.QueryParams.SearchText) ||
                     x.EqupmentName.Name.Contains(request.QueryParams.SearchText) ||
                     x.BudgetCode.Contains(request.QueryParams.SearchText) ||
                     x.Remarks.Contains(request.QueryParams.SearchText) ||
                     (searchDate && x.AIPApprovalDate == dateSearch) ||
                     (searchDate && x.SentForAIPDate == dateSearch) ||
                     (searchDate && x.TenderFloatedDate == dateSearch) ||
                     (searchDate && x.OfferReceivedDateAndUpdateEvaluation == dateSearch) ||
                     (searchDate && x.SentForContractDate == dateSearch) ||
                     (searchDate && x.ContractSignedDate == dateSearch) ||
                     x.ProcurementBaseSchoolNames.Any(b => b.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText)),
                "GroupName", "EqupmentName", "Controlled", "FcLc", "DgdpNssd", "FinancialYear", "ProcurementBaseSchoolNames", "ProcurementBaseSchoolNames.BaseSchoolName"
            );

            // Apply custom ordering
            var orderedQuery = procurementsQuery;

            if (request.OrderBy != null)
            {
                switch (request.OrderBy.ToLower())
                {
                    case "equpmentname":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.EqupmentName.Name)
                            : orderedQuery.OrderBy(x => x.EqupmentName.Name);
                        break;
                    case "groupname":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.GroupName.Name)
                            : orderedQuery.OrderBy(x => x.GroupName.Name);
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
                    case "fclc":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.FcLc.Name)
                            : orderedQuery.OrderBy(x => x.FcLc.Name);
                        break;
                    case "controlled":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.Controlled.Name)
                            : orderedQuery.OrderBy(x => x.Controlled.Name);
                        break;

                    case "baseschoolname":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.ProcurementBaseSchoolNames
                                .OrderByDescending(b => b.BaseSchoolName.SchoolName)
                                .Select(b => b.BaseSchoolName.SchoolName)
                                .FirstOrDefault())
                            : orderedQuery.OrderBy(x => x.ProcurementBaseSchoolNames
                                .OrderBy(b => b.BaseSchoolName.SchoolName)
                                .Select(b => b.BaseSchoolName.SchoolName)
                                .FirstOrDefault());
                        break;

                    case "procurementid":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                           ? orderedQuery.OrderByDescending(x => x.ProcurementId)
                           : orderedQuery.OrderBy(x => x.ProcurementId);
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
                    case "eprice":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                           ? orderedQuery.OrderByDescending(x => x.EPrice)
                           : orderedQuery.OrderBy(x => x.EPrice);
                        break;

                    case "remarks":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                           ? orderedQuery.OrderByDescending(x => x.BudgetCode)
                           : orderedQuery.OrderBy(x => x.BudgetCode);
                        break;

                    case "aipapprovaldate":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.AIPApprovalDate)
                            : orderedQuery.OrderBy(x => x.AIPApprovalDate);
                        break;
                    case "sentforaip":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.SentForAIPDate)
                            : orderedQuery.OrderBy(x => x.SentForAIPDate);
                        break;
                    case "indentsentdate":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.IndentSentDate)
                            : orderedQuery.OrderBy(x => x.IndentSentDate);
                        break;
                    case "tenderfloateddate":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.TenderFloatedDate)
                            : orderedQuery.OrderBy(x => x.TenderFloatedDate);
                        break;
                    case "tenderopeningdate":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.ProcurementTenderOpenings
                                .OrderByDescending(b => b.TenderOpeningDate)
                                .Select(b => b.TenderOpeningDate)
                                .FirstOrDefault())
                            : orderedQuery.OrderBy(x => x.ProcurementTenderOpenings
                                .OrderBy(b => b.TenderOpeningDate)
                                .Select(b => b.TenderOpeningDate)
                                .FirstOrDefault());
                        break;
                    case "offerreceiveddateunderevaluation":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.OfferReceivedDateAndUpdateEvaluation)
                            : orderedQuery.OrderBy(x => x.OfferReceivedDateAndUpdateEvaluation);
                        break;
                    case "sentforcontract":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.SentForContractDate)
                            : orderedQuery.OrderBy(x => x.SentForContractDate);
                        break;
                    case "contractsigneddate":
                        orderedQuery = request.OrderDirection?.ToLower() == "desc"
                            ? orderedQuery.OrderByDescending(x => x.ContractSignedDate)
                            : orderedQuery.OrderBy(x => x.ContractSignedDate);
                        break;


                    default:
                        orderedQuery = orderedQuery.OrderByDescending(x => x.ProcurementId);
                        break;
                }

            }
            else
            {
                // Default ordering
                orderedQuery = orderedQuery.OrderByDescending(x => x.ProcurementId);
            }

            var totalCount = await orderedQuery.CountAsync();

            var procurements = await orderedQuery
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToListAsync();

            var baseSchoolNames = await _ProcurementBaseSchoolNameRepository.GetAll();
            var tenderOpenings = await _ProcurementTenderOpeningRepository.GetAll();
            var allBaseSchool = await _BaseSchoolNameRepository.GetAll();

            var procurementDtos = _mapper.Map<List<ProcurementDto>>(procurements);

            foreach (var procurementDto in procurementDtos)
            {
                procurementDto.BaseSchoolNameDtos = baseSchoolNames
                    .Where(x => x.ProcurementId == procurementDto.ProcurementId)
                    .Select(x => new ProcurementBaseSchoolNameDto
                    {
                        BaseSchoolNameId = x.BaseSchoolNameId,
                        BaseSchoolName = allBaseSchool.FirstOrDefault(b => b.BaseSchoolNameId == x.BaseSchoolNameId)?.SchoolName
                    }).ToList();

                procurementDto.ProcurementTenderOpeningDto = tenderOpenings
                    .Where(x => x.ProcurementId == procurementDto.ProcurementId)
                    .Select(x => new ProcurementTenderOpeningDto
                    {
                        TenderOpeningDate = x.TenderOpeningDate,
                        TenderOpeningCount = x.TenderOpeningCount
                    }).ToList();
            }

            var result = new PagedResult<ProcurementDto>(
                procurementDtos,
                totalCount,
                request.QueryParams.PageNumber,
                request.QueryParams.PageSize
            );

            return result;
        }


    }
}
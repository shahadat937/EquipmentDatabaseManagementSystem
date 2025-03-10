using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetPieCharteDataRequestHandler : IRequestHandler<GetPieCharteDataRequest, List<PieChartDataDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;

        public GetPieCharteDataRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository, IMapper mapper)
        {
            _ProcurementRepository = ProcurementRepository;
            _mapper = mapper;

        }
        public Task<List<PieChartDataDto>> Handle(GetPieCharteDataRequest request, CancellationToken cancellationToken)
        {
            // Get the two most recent financial years
            var financialYears = _ProcurementRepository
                .FilterWithInclude(x => true)
                .Select(x => x.FinancialYear.FinancialYearName)
                .Distinct()
                .OrderByDescending(x => x)
                .Take(2)
                .ToList();

            // Filter procurement records for the two most recent years
            var procurementsQuery = _ProcurementRepository.FilterWithInclude(
                x => financialYears.Contains(x.FinancialYear.FinancialYearName),
                "EqupmentName", "Controlled", "FcLc", "DgdpNssd", "FinancialYear",
                "ProcurementBaseSchoolNames", "ProcurementBaseSchoolNames.BaseSchoolName"
            );

            var procurements = procurementsQuery.ToList();

            // Group procurements by financial year and calculate counts
            var result = procurements
                .GroupBy(p => p.FinancialYear.FinancialYearName)
                .Select(group => new PieChartDataDto
                {
                    FinancialYear = group.Key,
                    TotalProcurementCount = group.Count(),
                    TotalContractSignedProcurementCount = group.Count(p => p.ContractSignedDate != null)
                })
                .ToList();


            var twoYearResults = new List<PieChartDataDto>();

            foreach (var year in financialYears)
            {
                var yearData = result.FirstOrDefault(r => r.FinancialYear == year);
                if (yearData == null)
                {
                    // If no data for this year, set default values
                    twoYearResults.Add(new PieChartDataDto
                    {
                        FinancialYear = year,
                        TotalProcurementCount = 0,
                        TotalContractSignedProcurementCount = 0
                    });
                }
                else
                {
                    // If data exists for this year, add the result
                    twoYearResults.Add(yearData);
                }
            }

            // If fewer than 2 results exist (e.g., no procurements for any of the two years), ensure we have 2 items
            if (twoYearResults.Count < 2)
            {
                while (twoYearResults.Count < 2)
                {
                    twoYearResults.Add(new PieChartDataDto
                    {
                        FinancialYear = "-",
                        TotalProcurementCount = 0,
                        TotalContractSignedProcurementCount = 0
                    });
                }
            }

            return Task.FromResult(twoYearResults);
        }


    }
}

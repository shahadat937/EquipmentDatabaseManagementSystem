using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReportingMonths;
using SchoolManagement.Application.DTOs.ShipInformationTypes;
using SchoolManagement.Application.Features.ReportingMonths.Requests.Queries;
using SchoolManagement.Doamin;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ReportingMonths.Handlers.Queries
{
    public class GetReportingMonthDetailRequestHandler : IRequestHandler<GetReportingMonthDetailRequest, ReportingMonthDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ReportingMonth> _ReportingMonthRepository;
        public GetReportingMonthDetailRequestHandler(ISchoolManagementRepository<ReportingMonth> ReportingMonthRepository, IMapper mapper)
        {
            _ReportingMonthRepository = ReportingMonthRepository;
            _mapper = mapper;
        }
        public async Task<ReportingMonthDto> Handle(GetReportingMonthDetailRequest request, CancellationToken cancellationToken)
        {
            var ReportingMonth = await _ReportingMonthRepository.Get(request.ReportingMonthId);
            return _mapper.Map<ReportingMonthDto>(ReportingMonth);
        }
    }
}

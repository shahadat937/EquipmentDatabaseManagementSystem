using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReportingYear;
using SchoolManagement.Application.Features.YearSetups.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ReportingYears.Handlers.Queries
{
    public class GetReportingYearDetailRequestHandler : IRequestHandler<GetReportingYearDetailRequest, ReportingYearDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ReportingYear> _ReportingYearRepository;
        public GetReportingYearDetailRequestHandler(ISchoolManagementRepository<ReportingYear> ReportingYearRepository, IMapper mapper)
        {
            _ReportingYearRepository = ReportingYearRepository;
            _mapper = mapper;
        }
        public async Task<ReportingYearDto> Handle(GetReportingYearDetailRequest request, CancellationToken cancellationToken)
        {
            var ReportingYear = await _ReportingYearRepository.Get(request.ReportingYearId);
            return _mapper.Map<ReportingYearDto>(ReportingYear);
        }
    }
}

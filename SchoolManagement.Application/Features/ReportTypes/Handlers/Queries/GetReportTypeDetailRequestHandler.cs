using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReportType;
using SchoolManagement.Application.Features.ReportTypes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ReportTypes.Handlers.Queries
{
    public class GetReportTypeDetailRequestHandler : IRequestHandler<GetReportTypeDetailRequest, ReportTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ReportType> _ReportTypeRepository;
        public GetReportTypeDetailRequestHandler(ISchoolManagementRepository<ReportType> ReportTypeRepository, IMapper mapper)
        {
            _ReportTypeRepository = ReportTypeRepository;
            _mapper = mapper;
        }
        public async Task<ReportTypeDto> Handle(GetReportTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var ReportType = await _ReportTypeRepository.Get(request.ReportTypeId);
            return _mapper.Map<ReportTypeDto>(ReportType);
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.OperationalStatuss;
using SchoolManagement.Application.Features.OperationalStatuss.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatuss.Handlers.Queries
{
    public class GetOperationalStatusDetailRequestHandler : IRequestHandler<GetOperationalStatusDetailRequest, OperationalStatusDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<OperationalStatus> _OperationalStatusRepository;
        public GetOperationalStatusDetailRequestHandler(ISchoolManagementRepository<OperationalStatus> OperationalStatusRepository, IMapper mapper)
        {
            _OperationalStatusRepository = OperationalStatusRepository;
            _mapper = mapper;
        }
        public async Task<OperationalStatusDto> Handle(GetOperationalStatusDetailRequest request, CancellationToken cancellationToken)
        {
            var OperationalStatus = await _OperationalStatusRepository.Get(request.OperationalStatusId);
            return _mapper.Map<OperationalStatusDto>(OperationalStatus);
        }
    }
}

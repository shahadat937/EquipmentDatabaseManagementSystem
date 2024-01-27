using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EqupmentNames;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.EqupmentNames.Handlers.Queries
{
    public class GetEqupmentNameDetailRequestHandler : IRequestHandler<GetEqupmentNameDetailRequest, EqupmentNameDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<EqupmentName> _EqupmentNameRepository;
        public GetEqupmentNameDetailRequestHandler(ISchoolManagementRepository<EqupmentName> EqupmentNameRepository, IMapper mapper)
        {
            _EqupmentNameRepository = EqupmentNameRepository;
            _mapper = mapper;
        }
        public async Task<EqupmentNameDto> Handle(GetEqupmentNameDetailRequest request, CancellationToken cancellationToken)
        {
            var EqupmentName = await _EqupmentNameRepository.Get(request.EqupmentNameId);
            return _mapper.Map<EqupmentNameDto>(EqupmentName);
        }
    }
}

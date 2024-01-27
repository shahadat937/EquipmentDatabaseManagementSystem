using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Sqns;
using SchoolManagement.Application.Features.Sqns.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Sqns.Handlers.Queries
{
    public class GetSqnDetailRequestHandler : IRequestHandler<GetSqnDetailRequest, SqnDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Sqn> _SqnRepository;
        public GetSqnDetailRequestHandler(ISchoolManagementRepository<Sqn> SqnRepository, IMapper mapper)
        {
            _SqnRepository = SqnRepository;
            _mapper = mapper;
        }
        public async Task<SqnDto> Handle(GetSqnDetailRequest request, CancellationToken cancellationToken)
        {
            var Sqn = await _SqnRepository.Get(request.SqnId);
            return _mapper.Map<SqnDto>(Sqn);
        }
    }
}

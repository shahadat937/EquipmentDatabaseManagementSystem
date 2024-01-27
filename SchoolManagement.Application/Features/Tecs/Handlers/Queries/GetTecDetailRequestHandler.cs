using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Tec;
using SchoolManagement.Application.Features.Tecs.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Tecs.Handlers.Queries
{
    public class GetTecDetailRequestHandler : IRequestHandler<GetTecDetailRequest, TecDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Tec> _TecRepository;
        public GetTecDetailRequestHandler(ISchoolManagementRepository<Tec> TecRepository, IMapper mapper)
        {
            _TecRepository = TecRepository;
            _mapper = mapper;
        }
        public async Task<TecDto> Handle(GetTecDetailRequest request, CancellationToken cancellationToken)
        {
            var Tec = await _TecRepository.Get(request.TecId);
            return _mapper.Map<TecDto>(Tec);
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Envelope;
using SchoolManagement.Application.Features.Envelopes.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Envelopes.Handlers.Queries
{
    public class GetEnvelopeDetailRequestHandler : IRequestHandler<GetEnvelopeDetailRequest, EnvelopeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Envelope> _EnvelopeRepository;
        public GetEnvelopeDetailRequestHandler(ISchoolManagementRepository<Envelope> EnvelopeRepository, IMapper mapper)
        {
            _EnvelopeRepository = EnvelopeRepository;
            _mapper = mapper;
        }
        public async Task<EnvelopeDto> Handle(GetEnvelopeDetailRequest request, CancellationToken cancellationToken)
        {
            var Envelope = await _EnvelopeRepository.Get(request.EnvelopeId);
            return _mapper.Map<EnvelopeDto>(Envelope);
        }
    }
}

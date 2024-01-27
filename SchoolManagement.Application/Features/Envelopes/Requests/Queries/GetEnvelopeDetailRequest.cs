using MediatR;
using SchoolManagement.Application.DTOs.Envelope;

namespace SchoolManagement.Application.Features.Envelopes.Requests.Queries
{
    public class GetEnvelopeDetailRequest : IRequest<EnvelopeDto>
    {
        public int EnvelopeId { get; set; }
    }
}

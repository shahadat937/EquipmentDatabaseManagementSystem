using MediatR;
using SchoolManagement.Application.DTOs.Envelope;

namespace SchoolManagement.Application.Features.Envelopes.Requests.Commands
{
    public class UpdateEnvelopeCommand : IRequest<Unit>
    { 
        public EnvelopeDto EnvelopeDto { get; set; }
    }
}
 
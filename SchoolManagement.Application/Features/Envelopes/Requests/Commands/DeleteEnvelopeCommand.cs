using MediatR;

namespace SchoolManagement.Application.Features.Envelopes.Requests.Commands
{
    public class DeleteEnvelopeCommand : IRequest
    {
        public int EnvelopeId { get; set; }
    }
} 

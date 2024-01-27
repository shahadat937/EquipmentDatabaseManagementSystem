using MediatR;
using SchoolManagement.Application.DTOs.Envelope;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Envelopes.Requests.Commands
{
    public class CreateEnvelopeCommand : IRequest<BaseCommandResponse>
    {
        public CreateEnvelopeDto EnvelopeDto { get; set; }
    }
}

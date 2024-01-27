using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Envelopes.Requests.Queries
{
    public class GetSelectedEnvelopeRequest : IRequest<List<SelectedModel>>
    {
    }
} 

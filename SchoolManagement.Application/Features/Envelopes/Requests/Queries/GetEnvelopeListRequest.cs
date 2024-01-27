using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Envelope;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Envelopes.Requests.Queries
{
    public class GetEnvelopeListRequest : IRequest<PagedResult<EnvelopeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Envelopes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Envelopes.Handlers.Queries
{
    public class GetSelectedEnvelopeRequestHandler : IRequestHandler<GetSelectedEnvelopeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Envelope> _EnvelopeRepository;


        public GetSelectedEnvelopeRequestHandler(ISchoolManagementRepository<Envelope> EnvelopeRepository)
        {
            _EnvelopeRepository = EnvelopeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEnvelopeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Envelope> codeValues = await _EnvelopeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.EnvelopeId
            }).ToList();
            return selectModels;
        }
    }
}

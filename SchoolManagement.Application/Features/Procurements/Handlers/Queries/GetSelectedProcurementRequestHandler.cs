using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetSelectedProcurementRequestHandler : IRequestHandler<GetSelectedProcurementRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;


        public GetSelectedProcurementRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository)
        {
            _ProcurementRepository = ProcurementRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedProcurementRequest request, CancellationToken cancellationToken)
        {
            ICollection<Procurement> codeValues = await _ProcurementRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.AIP,
                Value = x.ProcurementId
            }).ToList();
            return selectModels;
        }
    }
}

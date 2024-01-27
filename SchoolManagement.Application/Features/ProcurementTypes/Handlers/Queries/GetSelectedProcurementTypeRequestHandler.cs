using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ProcurementTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProcurementTypes.Handlers.Queries
{
    public class GetSelectedProcurementTypeRequestHandler : IRequestHandler<GetSelectedProcurementTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ProcurementType> _ProcurementTypeRepository;


        public GetSelectedProcurementTypeRequestHandler(ISchoolManagementRepository<ProcurementType> ProcurementTypeRepository)
        {
            _ProcurementTypeRepository = ProcurementTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedProcurementTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<ProcurementType> codeValues = await _ProcurementTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ProcurementTypeId
            }).ToList();
            return selectModels;
        }
    }
}

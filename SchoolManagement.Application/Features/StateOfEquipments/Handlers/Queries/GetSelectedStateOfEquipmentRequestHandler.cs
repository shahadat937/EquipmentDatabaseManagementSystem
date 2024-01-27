using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.StateOfEquipments.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.StateOfEquipments.Handlers.Queries
{
    public class GetSelectedStateOfEquipmentRequestHandler : IRequestHandler<GetSelectedStateOfEquipmentRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<StateOfEquipment> _StateOfEquipmentRepository;


        public GetSelectedStateOfEquipmentRequestHandler(ISchoolManagementRepository<StateOfEquipment> StateOfEquipmentRepository)
        {
            _StateOfEquipmentRepository = StateOfEquipmentRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedStateOfEquipmentRequest request, CancellationToken cancellationToken)
        {
            ICollection<StateOfEquipment> codeValues = await _StateOfEquipmentRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.StateOfEquipmentId
            }).ToList();
            return selectModels;
        }
    }
}

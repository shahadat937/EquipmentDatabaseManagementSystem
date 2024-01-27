using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShipTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShipTypes.Handlers.Queries
{
    public class GetSelectedShipTypeRequestHandler : IRequestHandler<GetSelectedShipTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ShipType> _ShipTypeRepository;


        public GetSelectedShipTypeRequestHandler(ISchoolManagementRepository<ShipType> ShipTypeRepository)
        {
            _ShipTypeRepository = ShipTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedShipTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<ShipType> codeValues = await _ShipTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ShipTypeId
            }).ToList();
            return selectModels;
        }
    }
}

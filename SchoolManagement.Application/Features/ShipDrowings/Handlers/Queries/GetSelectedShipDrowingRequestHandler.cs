using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShipDrowings.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShipDrowings.Handlers.Queries
{
    public class GetSelectedShipDrowingRequestHandler : IRequestHandler<GetSelectedShipDrowingRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ShipDrowing> _ShipDrowingRepository;


        public GetSelectedShipDrowingRequestHandler(ISchoolManagementRepository<ShipDrowing> ShipDrowingRepository)
        {
            _ShipDrowingRepository = ShipDrowingRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedShipDrowingRequest request, CancellationToken cancellationToken)
        {
            ICollection<ShipDrowing> codeValues = await _ShipDrowingRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ShipDrowingId
            }).ToList();
            return selectModels;
        }
    }
}

using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShipInformations.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Queries
{
    public class GetSelectedShipInformationRequestHandler : IRequestHandler<GetSelectedShipInformationRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ShipInformation> _ShipInformationRepository;


        public GetSelectedShipInformationRequestHandler(ISchoolManagementRepository<ShipInformation> ShipInformationRepository)
        {
            _ShipInformationRepository = ShipInformationRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedShipInformationRequest request, CancellationToken cancellationToken)
        {
            ICollection<ShipInformation> codeValues = await _ShipInformationRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Address,
                Value = x.ShipInformationId
            }).ToList();
            return selectModels;
        }
    }
}

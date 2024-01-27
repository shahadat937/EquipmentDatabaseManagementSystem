using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DealingOfficers.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DealingOfficers.Handlers.Queries
{
    public class GetSelectedDealingOfficerRequestHandler : IRequestHandler<GetSelectedDealingOfficerRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<DealingOfficer> _DealingOfficerRepository;


        public GetSelectedDealingOfficerRequestHandler(ISchoolManagementRepository<DealingOfficer> DealingOfficerRepository)
        {
            _DealingOfficerRepository = DealingOfficerRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDealingOfficerRequest request, CancellationToken cancellationToken)
        {
            ICollection<DealingOfficer> codeValues = await _DealingOfficerRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.DealingOfficerId
            }).ToList();
            return selectModels;
        }
    }
}

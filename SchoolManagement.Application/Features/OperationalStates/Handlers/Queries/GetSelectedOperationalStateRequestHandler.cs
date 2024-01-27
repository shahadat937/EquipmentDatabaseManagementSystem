using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.OperationalStates.Requests.Queries;
using SchoolManagement.Doamin;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.OperationalStates.Handlers.Queries
{
    public class GetSelectedOperationalStateRequestHandler : IRequestHandler<GetSelectedOperationalStateRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<OperationalState> _OperationalStateRepository;


        public GetSelectedOperationalStateRequestHandler(ISchoolManagementRepository<OperationalState> OperationalStateRepository)
        {
            _OperationalStateRepository = OperationalStateRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedOperationalStateRequest request, CancellationToken cancellationToken)
        {
            ICollection<OperationalState> codeValues = await _OperationalStateRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.OperationalStateId,
                Value = x.OperationalStateId
            }).ToList();
            return selectModels;
        }
    }
}

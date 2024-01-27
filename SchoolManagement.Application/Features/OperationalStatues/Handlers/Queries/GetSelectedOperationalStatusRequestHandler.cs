using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.OperationalStatuss.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.OperationalStatuss.Handlers.Queries
{
    public class GetSelectedOperationalStatusRequestHandler : IRequestHandler<GetSelectedOperationalStatusRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<OperationalStatus> _OperationalStatusRepository;


        public GetSelectedOperationalStatusRequestHandler(ISchoolManagementRepository<OperationalStatus> OperationalStatusRepository)
        {
            _OperationalStatusRepository = OperationalStatusRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedOperationalStatusRequest request, CancellationToken cancellationToken)
        {
            ICollection<OperationalStatus> codeValues = await _OperationalStatusRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.OperationalStatusId
            }).ToList();
            return selectModels;
        }
    }
}

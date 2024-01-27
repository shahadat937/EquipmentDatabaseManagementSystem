using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Sqns.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Sqns.Handlers.Queries
{
    public class GetSelectedSqnRequestHandler : IRequestHandler<GetSelectedSqnRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Sqn> _SqnRepository;


        public GetSelectedSqnRequestHandler(ISchoolManagementRepository<Sqn> SqnRepository)
        {
            _SqnRepository = SqnRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSqnRequest request, CancellationToken cancellationToken)
        {
            ICollection<Sqn> codeValues = await _SqnRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.SqnId
            }).ToList();
            return selectModels;
        }
    }
}

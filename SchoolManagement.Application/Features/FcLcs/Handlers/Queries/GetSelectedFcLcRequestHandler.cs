using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FcLcs.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FcLcs.Handlers.Queries
{
    public class GetSelectedFcLcRequestHandler : IRequestHandler<GetSelectedFcLcRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<FcLc> _FcLcRepository;


        public GetSelectedFcLcRequestHandler(ISchoolManagementRepository<FcLc> FcLcRepository)
        {
            _FcLcRepository = FcLcRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFcLcRequest request, CancellationToken cancellationToken)
        {
            ICollection<FcLc> codeValues = await _FcLcRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.FcLcId
            }).ToList();
            return selectModels;
        }
    }
}

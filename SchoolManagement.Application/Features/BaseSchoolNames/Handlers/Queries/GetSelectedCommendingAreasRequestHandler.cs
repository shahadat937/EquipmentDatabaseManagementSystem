using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Queries
{ 
    public class GetSelectedCommendingAreasRequestHandler : IRequestHandler<GetSelectedCommendingAreasRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<BaseSchoolName> _SchoolNameRepository;


        public GetSelectedCommendingAreasRequestHandler(ISchoolManagementRepository<BaseSchoolName> SchoolNameRepository)
        {
            _SchoolNameRepository = SchoolNameRepository; 
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedCommendingAreasRequest request, CancellationToken cancellationToken)
        {
            ICollection<BaseSchoolName> BaseSchoolNames = await _SchoolNameRepository.FilterAsync(x => x.BranchLevel == 2 && x.FirstLevel == request.FirstLevel);
            List<SelectedModel> selectModels = BaseSchoolNames.Select(x => new SelectedModel 
            {
                Text = x.SchoolName,
                Value = x.BaseSchoolNameId
            }).ToList();
            return selectModels;
        }
    }
}

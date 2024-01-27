using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.CourseDurations.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Queries
{
    public class GetBaseSchoolNameByBaseNameIdRequestHandler : IRequestHandler<GetBaseSchoolNameByBaseNameIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;

          
        public GetBaseSchoolNameByBaseNameIdRequestHandler(ISchoolManagementRepository<BaseSchoolName> BaseSchoolNameRepository)
        {
            _BaseSchoolNameRepository = BaseSchoolNameRepository;           
        }

        public async Task<List<SelectedModel>> Handle(GetBaseSchoolNameByBaseNameIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<BaseSchoolName> BaseSchoolNames = await _BaseSchoolNameRepository.FilterAsync(x =>x.BaseNameId == request.BaseNameId);
            List<SelectedModel> selectModels = BaseSchoolNames.Select(x => new SelectedModel
            {
                Text = x.SchoolName, 
                Value = x.BaseSchoolNameId 
            }).ToList();
            return selectModels;
        }
    }
}

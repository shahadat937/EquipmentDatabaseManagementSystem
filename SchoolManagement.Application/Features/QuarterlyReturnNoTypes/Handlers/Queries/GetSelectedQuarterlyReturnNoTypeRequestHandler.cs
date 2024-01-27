using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Handlers.Queries
{
    public class GetSelectedQuarterlyReturnNoTypeRequestHandler : IRequestHandler<GetSelectedQuarterlyReturnNoTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<QuarterlyReturnNoType> _QuarterlyReturnNoTypeRepository;


        public GetSelectedQuarterlyReturnNoTypeRequestHandler(ISchoolManagementRepository<QuarterlyReturnNoType> QuarterlyReturnNoTypeRepository)
        {
            _QuarterlyReturnNoTypeRepository = QuarterlyReturnNoTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedQuarterlyReturnNoTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<QuarterlyReturnNoType> codeValues = await _QuarterlyReturnNoTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.QuarterlyReturnNoTypeId
            }).ToList();
            return selectModels;
        }
    }
}

using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Handlers.Queries
{
    public class GetSelectedTenderOpeningDateTypeRequestHandler : IRequestHandler<GetSelectedTenderOpeningDateTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<TenderOpeningDateType> _TenderOpeningDateTypeRepository;


        public GetSelectedTenderOpeningDateTypeRequestHandler(ISchoolManagementRepository<TenderOpeningDateType> TenderOpeningDateTypeRepository)
        {
            _TenderOpeningDateTypeRepository = TenderOpeningDateTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedTenderOpeningDateTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<TenderOpeningDateType> codeValues = await _TenderOpeningDateTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.TenderOpeningDateTypeId
            }).ToList();
            return selectModels;
        }
    }
}

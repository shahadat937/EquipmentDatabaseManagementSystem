using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Handlers.Queries
{
    public class GetSelectedHalfYearlyReturnRequestHandler : IRequestHandler<GetSelectedHalfYearlyReturnRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<HalfYearlyReturn> _HalfYearlyReturnRepository;


        public GetSelectedHalfYearlyReturnRequestHandler(ISchoolManagementRepository<HalfYearlyReturn> HalfYearlyReturnRepository)
        {
            _HalfYearlyReturnRepository = HalfYearlyReturnRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedHalfYearlyReturnRequest request, CancellationToken cancellationToken)
        {
            ICollection<HalfYearlyReturn> codeValues = await _HalfYearlyReturnRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.InputPowerSupply,
                Value = x.HalfYearlyReturnId
            }).ToList();
            return selectModels;
        }
    }
}

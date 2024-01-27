using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Brands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Brands.Handlers.Queries
{
    public class GetSelectedBrandRequestHandler : IRequestHandler<GetSelectedBrandRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Brand> _BrandRepository;


        public GetSelectedBrandRequestHandler(ISchoolManagementRepository<Brand> BrandRepository)
        {
            _BrandRepository = BrandRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBrandRequest request, CancellationToken cancellationToken)
        {
            ICollection<Brand> codeValues = await _BrandRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.BrandId
            }).ToList();
            return selectModels;
        }
    }
}

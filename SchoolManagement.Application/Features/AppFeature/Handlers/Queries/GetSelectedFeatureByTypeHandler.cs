using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.AppFeature.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.AppFeature.Handlers.Queries
{
    public class GetSelectedFeatureByTypeHandler : IRequestHandler<GetSelectedFeatureByTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Feature> _FeatureRepository;


        public GetSelectedFeatureByTypeHandler(ISchoolManagementRepository<Feature> FeatureRepository)
        {
            _FeatureRepository = FeatureRepository;           
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFeatureByTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Feature> Features = await _FeatureRepository.FilterAsync(x =>x.IsActive);
            List<SelectedModel> selectModels = Features.Select(x => new SelectedModel
            {
                Text = x.FeatureName,
                Value = x.FeatureId
            }).ToList();
            return selectModels;
        }
    }
}

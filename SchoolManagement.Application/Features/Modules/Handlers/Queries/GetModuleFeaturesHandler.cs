using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Application.DTOs.Modules;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Modules.Requests.Queries;
using SchoolManagement.Domain;
using System.Security.Claims;

namespace SchoolManagement.Application.Features.Modules.Handlers.Queries
{
    public class GetModuleFeaturesHandler : IRequestHandler<GetModuleFeaturesRequests, List<ModuleFeatureDto>>
    {
        private readonly ISchoolManagementRepository<Module> _ModuleRepository;
        private readonly ISchoolManagementRepository<Feature> _FeatureRepository;
        private readonly ISchoolManagementRepository<RoleFeature> _RoleFeatureRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetModuleFeaturesHandler(ISchoolManagementRepository<Feature> FeatureRepository,ISchoolManagementRepository<RoleFeature> RoleFeatureRepository, ISchoolManagementRepository<Module> ModuleRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _ModuleRepository = ModuleRepository;
            _RoleFeatureRepository = RoleFeatureRepository;
            _FeatureRepository = FeatureRepository;
            _mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ModuleFeatureDto>> Handle(GetModuleFeaturesRequests request, CancellationToken cancellationToken)
        {
              
            var rId = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value ;
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            ICollection<RoleFeature> roleFeature =await _RoleFeatureRepository.FilterAsync(x =>x.RoleId == rId);
            int[] featureIds = roleFeature.Select(x => x.FeatureKey).Distinct().ToArray();
            List<Feature> feautres = _FeatureRepository.FilterWithInclude(x => x.IsActive && x.FeatureTypeId == request.FeatureType && featureIds.Contains(x.FeatureId), "Module").ToList();
            List<Module> modeules = feautres.GroupBy(x => x.Module.ModuleName).Select(x => x.First().Module).OrderBy(x => x.MenuPosition).ToList();
            if (!featureIds.Any())
            {
                throw new BadRequestException("Features Not Assigned");
            }

            List<ModuleFeatureDto> moduleFeatureDto = _mapper.Map<List<ModuleFeatureDto>>(modeules);
            List<FeatureDto> featureDtos = _mapper.Map<List<FeatureDto>>(feautres);

            moduleFeatureDto = moduleFeatureDto.OrderBy(x => x.MenuPosition).Select(x =>
            {
                x.Features = featureDtos.Where(y => y.ModuleId == x.ModuleId).OrderBy(o => o.OrderNo).ToList();
                x.Role = role;
                return x;
            }).ToList();

            return moduleFeatureDto;
        }
    }
}

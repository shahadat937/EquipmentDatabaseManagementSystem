using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Features.RoleFeatures.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.RoleFeatures.Handler.Queries
{
    public class GetRoleFeatureListRequestHandler : IRequestHandler<GetRoleFeatureListRequest, PagedResult<RoleFeatureDto>>
    {

        private readonly ISchoolManagementRepository<RoleFeature> _branchRepository;
        private readonly ISchoolManagementRepository<AspNetRoles> _roleRepository;
        private readonly ISchoolManagementRepository<Feature> _featureRepository;

        private readonly IMapper _mapper;

        public GetRoleFeatureListRequestHandler(ISchoolManagementRepository<RoleFeature> branchRepository, IMapper mapper, ISchoolManagementRepository<AspNetRoles> roleRepository, ISchoolManagementRepository<Feature> featureRepository)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _featureRepository = featureRepository;
        }

        //public async Task<PagedResult<RoleFeatureDto>> Handle(GetRoleFeatureListRequest request, CancellationToken cancellationToken)
        //{
        //    var validator = new QueryParamsValidator();
        //    var validationResult = await validator.ValidateAsync(request.QueryParams);

        //    if (validationResult.IsValid == false)
        //        throw new ValidationException(validationResult.ToString());

        //     IQueryable<RoleFeature> branches = _branchRepository.FilterWithInclude(x => String.IsNullOrEmpty(request.QueryParams.SearchText));
        //    var totalCount = branches.Count();
        //    branches = branches.OrderByDescending(x => x.RoleId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

        //    var branchesDtos = _mapper.Map<List<RoleFeatureDto>>(branches);
        //    var result = new PagedResult<RoleFeatureDto>(branchesDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

        //    return result;
        //}
        public async Task<PagedResult<RoleFeatureDto>> Handle(GetRoleFeatureListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString());

            var rolesCollection = await _roleRepository.GetAll();
            var roles = rolesCollection
                        .Where(r => string.IsNullOrEmpty(request.QueryParams.SearchText) ||
                                    r.Name.Contains(request.QueryParams.SearchText)) // Filter by RoleName
                        .ToDictionary(r => r.Id, r => r.Name);

            var roleIds = roles.Keys.ToList();

            IQueryable<RoleFeature> branches = _branchRepository.FilterWithInclude(x => roleIds.Contains(x.RoleId));

            var totalCount = branches.Count();

            branches = branches.OrderByDescending(x => x.RoleId)
                               .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                               .Take(request.QueryParams.PageSize);

            var featureKeys = branches.Select(x => x.FeatureKey).Distinct().ToList();
            var features = _featureRepository.Where(f => featureKeys.Contains(f.FeatureId))
                                             .ToDictionary(f => f.FeatureId, f => f.FeatureName);

            var branchesDtos = branches.Select(x => new RoleFeatureDto
            {
                RoleId = x.RoleId,
                FeatureKey = x.FeatureKey,
                Add = x.Add,
                Update = x.Update,
                Delete = x.Delete,
                Report = x.Report,
                RoleName = roles.ContainsKey(x.RoleId) ? roles[x.RoleId] : null,
                FeatureName = features.ContainsKey(x.FeatureKey) ? features[x.FeatureKey] : null
            }).ToList();

            var result = new PagedResult<RoleFeatureDto>(branchesDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }

    }
}

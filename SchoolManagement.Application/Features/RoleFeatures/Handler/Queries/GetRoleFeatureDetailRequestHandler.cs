using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.Features.RoleFeatures.Requests.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.RoleFeatures.Handler.Queries
{ 
    public class GetRoleFeatureDetailRequestHandler : IRequestHandler<GetRoleFeatureDetailRequest, RoleFeatureDto>
    {
        private readonly IMapper _mapper; 
        private readonly ISchoolManagementRepository<RoleFeature> _branchRepository; 
        public GetRoleFeatureDetailRequestHandler(ISchoolManagementRepository<RoleFeature> branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository; 
            _mapper = mapper;
        }
        public async Task<RoleFeatureDto> Handle(GetRoleFeatureDetailRequest request, CancellationToken cancellationToken)
        {
            var roleFeature = await _branchRepository.FindOneAsync(x => x.RoleId == request.RoleId && x.FeatureKey == request.FeatureId);
            return _mapper.Map<RoleFeatureDto>(roleFeature);
        }
    }
}

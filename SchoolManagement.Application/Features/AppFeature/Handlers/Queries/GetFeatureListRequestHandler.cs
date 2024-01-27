using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Application.Features.AppFeature.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.AppFeature.Handlers.Queries
{
    public class GetFeatureListRequestHandler : IRequestHandler<GetFeatureListRequest, PagedResult<FeatureDto>>
    { 

        private readonly ISchoolManagementRepository<Feature> _FeatureRepository;    

        private readonly IMapper _mapper;  
         
        public GetFeatureListRequestHandler(ISchoolManagementRepository<Feature> FeatureRepository, IMapper mapper)
        {
            _FeatureRepository = FeatureRepository; 
            _mapper = mapper;  
        }

        public async Task<PagedResult<FeatureDto>> Handle(GetFeatureListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString()); 

            IQueryable<Feature> Features = _FeatureRepository.FilterWithInclude(x => (x.FeatureName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Module");
            var totalCount = Features.Count();
            Features = Features.OrderBy(x => x.FeatureId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
              
            var FeaturesDtos = _mapper.Map<List<FeatureDto>>(Features); 
            var result = new PagedResult<FeatureDto>(FeaturesDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }
    }
}

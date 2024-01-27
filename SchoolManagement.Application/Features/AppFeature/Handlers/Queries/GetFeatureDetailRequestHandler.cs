using AutoMapper;
using MediatR;
using SchoolManagement.Application.Features.AppFeature.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.AppFeature.Handlers.Queries
{
    public class GetFeatureDetailRequestHandler : IRequestHandler<GetFeatureDetailRequest, FeatureDto>
    {
        private readonly IMapper _mapper;   
        private readonly ISchoolManagementRepository<Feature> _FeatureRepository;       
        public GetFeatureDetailRequestHandler(ISchoolManagementRepository<Feature> FeatureRepository, IMapper mapper)
        {
            _FeatureRepository = FeatureRepository;    
            _mapper = mapper; 
        } 
        public async Task<FeatureDto> Handle(GetFeatureDetailRequest request, CancellationToken cancellationToken)
        {
            var Feature = await _FeatureRepository.Get(request.Id);    
            return _mapper.Map<FeatureDto>(Feature);    
        }
    }
}

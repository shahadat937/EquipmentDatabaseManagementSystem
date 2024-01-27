using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Brands;
using SchoolManagement.Application.Features.Brands.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Brands.Handlers.Queries
{
    public class GetBrandDetailRequestHandler : IRequestHandler<GetBrandDetailRequest, BrandDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Brand> _BrandRepository;
        public GetBrandDetailRequestHandler(ISchoolManagementRepository<Brand> BrandRepository, IMapper mapper)
        {
            _BrandRepository = BrandRepository;
            _mapper = mapper;
        }
        public async Task<BrandDto> Handle(GetBrandDetailRequest request, CancellationToken cancellationToken)
        {
            var Brand = await _BrandRepository.Get(request.BrandId);
            return _mapper.Map<BrandDto>(Brand);
        }
    }
}

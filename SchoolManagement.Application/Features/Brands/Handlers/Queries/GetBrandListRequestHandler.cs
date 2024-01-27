using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Brands;
using SchoolManagement.Application.Features.Brands.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Brands.Handlers.Queries
{
    public class GetBrandListRequestHandler : IRequestHandler<GetBrandListRequest, PagedResult<BrandDto>>
    {

        private readonly ISchoolManagementRepository<Brand> _BrandRepository;

        private readonly IMapper _mapper;

        public GetBrandListRequestHandler(ISchoolManagementRepository<Brand> BrandRepository, IMapper mapper)
        {
            _BrandRepository = BrandRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BrandDto>> Handle(GetBrandListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Brand> Brands = _BrandRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Brands.Count();
            Brands = Brands.OrderByDescending(x => x.BrandId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var BrandDtos = _mapper.Map<List<BrandDto>>(Brands);
            var result = new PagedResult<BrandDto>(BrandDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

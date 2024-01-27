using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.EquipmentCategorys;
using SchoolManagement.Application.Features.EquipmentCategorys.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Handlers.Queries
{
    public class GetEquipmentCategoryListRequestHandler : IRequestHandler<GetEquipmentCategoryListRequest, PagedResult<EquipmentCategoryDto>>
    {

        private readonly ISchoolManagementRepository<EquipmentCategory> _EquipmentCategoryRepository;

        private readonly IMapper _mapper;

        public GetEquipmentCategoryListRequestHandler(ISchoolManagementRepository<EquipmentCategory> EquipmentCategoryRepository, IMapper mapper)
        {
            _EquipmentCategoryRepository = EquipmentCategoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EquipmentCategoryDto>> Handle(GetEquipmentCategoryListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<EquipmentCategory> EquipmentCategorys = _EquipmentCategoryRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "GroupName");
            var totalCount = EquipmentCategorys.Count();
            EquipmentCategorys = EquipmentCategorys.OrderBy(x => x.MenuPosition).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var EquipmentCategoryDtos = _mapper.Map<List<EquipmentCategoryDto>>(EquipmentCategorys);
            var result = new PagedResult<EquipmentCategoryDto>(EquipmentCategoryDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

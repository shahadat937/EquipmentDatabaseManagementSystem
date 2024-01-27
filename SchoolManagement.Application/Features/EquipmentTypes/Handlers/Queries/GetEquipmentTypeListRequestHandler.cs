using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.EquipmentType;
using SchoolManagement.Application.Features.EquipmentTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EquipmentTypes.Handlers.Queries
{
    public class GetEquipmentTypeListRequestHandler : IRequestHandler<GetEquipmentTypeListRequest, PagedResult<EquipmentTypeDto>>
    {

        private readonly ISchoolManagementRepository<EquipmentType> _EquipmentTypeRepository;

        private readonly IMapper _mapper;

        public GetEquipmentTypeListRequestHandler(ISchoolManagementRepository<EquipmentType> EquipmentTypeRepository, IMapper mapper)
        {
            _EquipmentTypeRepository = EquipmentTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<EquipmentTypeDto>> Handle(GetEquipmentTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<EquipmentType> EquipmentTypes = _EquipmentTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = EquipmentTypes.Count();
            EquipmentTypes = EquipmentTypes.OrderByDescending(x => x.EquipmentTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var EquipmentTypeDtos = _mapper.Map<List<EquipmentTypeDto>>(EquipmentTypes);
            var result = new PagedResult<EquipmentTypeDto>(EquipmentTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

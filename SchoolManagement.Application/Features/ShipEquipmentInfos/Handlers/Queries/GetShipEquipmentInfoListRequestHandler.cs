using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetShipEquipmentInfoListRequestHandler : IRequestHandler<GetShipEquipmentInfoListRequest, PagedResult<ShipEquipmentInfoDto>>
    {

        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _ShipEquipmentInfoRepository;

        private readonly IMapper _mapper;

        public GetShipEquipmentInfoListRequestHandler(ISchoolManagementRepository<ShipEquipmentInfo> ShipEquipmentInfoRepository, IMapper mapper)
        {
            _ShipEquipmentInfoRepository = ShipEquipmentInfoRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShipEquipmentInfoDto>> Handle(GetShipEquipmentInfoListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            //IQueryable<ShipEquipmentInfo> ShipEquipmentInfos = _ShipEquipmentInfoRepository.FilterWithInclude(x => x.BaseSchoolNameId == (request.ShipId != 0 ? request.ShipId : x.BaseSchoolNameId) && (x.Model.Contains(request.QueryParams.SearchText) || x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText) || x.Brand.Contains(request.QueryParams.SearchText) || x.EqupmentName.Name.Contains(request.QueryParams.SearchText) || x.EquipmentCategory.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "EquipmentCategory", "EqupmentName", "StateOfEquipment", "BaseSchoolName", "AcquisitionMethod");
            //var totalCount = ShipEquipmentInfos.Count();
            //ShipEquipmentInfos = ShipEquipmentInfos.OrderByDescending(x => x.ShipEquipmentInfoId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            //var ShipEquipmentInfoDtos = _mapper.Map<List<ShipEquipmentInfoDto>>(ShipEquipmentInfos);
            //var result = new PagedResult<ShipEquipmentInfoDto>(ShipEquipmentInfoDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            //return result;

            // Base query
            // Base query
            IQueryable<ShipEquipmentInfo> ShipEquipmentInfos = _ShipEquipmentInfoRepository.FilterWithInclude(
                x => x.BaseSchoolNameId == (request.ShipId != 0 ? request.ShipId : x.BaseSchoolNameId) &&
                (x.Model.Contains(request.QueryParams.SearchText) ||
                x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText) ||
                x.Brand.Contains(request.QueryParams.SearchText) ||
                x.EqupmentName.Name.Contains(request.QueryParams.SearchText) ||
                x.EquipmentCategory.Name.Contains(request.QueryParams.SearchText) ||
                string.IsNullOrEmpty(request.QueryParams.SearchText)),
                "EquipmentCategory", "EqupmentName", "StateOfEquipment", "BaseSchoolName", "AcquisitionMethod"
            );

            var totalCount = ShipEquipmentInfos.Count();

        
            var pagedData = ShipEquipmentInfos
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToList(); 


            var ShipEquipmentInfoDtos = _mapper.Map<List<ShipEquipmentInfoDto>>(pagedData);

            // Get sorting parameters
            string? capitilizeSortColumn =
                !string.IsNullOrEmpty(request.SortColumn)
                ? char.ToUpper(request.SortColumn[0]) + request.SortColumn.Substring(1)
                : "ShipEquipmentInfoId"; // Default column
            string sortColumn = capitilizeSortColumn; 
            bool isDescending = request.SortDirection?.ToLower() == "desc";


            ShipEquipmentInfoDtos = ApplyOrdering(ShipEquipmentInfoDtos, sortColumn, isDescending);

            var result = new PagedResult<ShipEquipmentInfoDto>(ShipEquipmentInfoDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }

        private static List<T> ApplyOrdering<T>(List<T> source, string propertyName, bool descending)
        {
            var entityType = typeof(T);
            var property = entityType.GetProperty(propertyName);

            if (property == null)
                throw new ArgumentException($"Property '{propertyName}' not found on type '{entityType.Name}'");

            return descending
                ? source.OrderByDescending(x => property.GetValue(x, null)).ToList()
                : source.OrderBy(x => property.GetValue(x, null)).ToList();
        }

    }
}

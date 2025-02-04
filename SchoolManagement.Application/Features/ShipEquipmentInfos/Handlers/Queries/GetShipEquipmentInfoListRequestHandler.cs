using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Domain;
using System.Linq.Expressions;

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

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            // Base query with filtering
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

            // Apply Sorting before Pagination
            if (!string.IsNullOrEmpty(request.SortColumn))
            {
                bool isDescending = request.SortDirection?.ToLower() == "desc";
                ShipEquipmentInfos = ApplyOrdering(ShipEquipmentInfos, request.SortColumn, isDescending);
            }
            else
            {
                // Default sorting by ShipEquipmentInfoId
                ShipEquipmentInfos = ShipEquipmentInfos.OrderByDescending(x => x.ShipEquipmentInfoId);
            }

            // Apply Pagination after sorting
            var pagedData = ShipEquipmentInfos
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToList();

            var ShipEquipmentInfoDtos = _mapper.Map<List<ShipEquipmentInfoDto>>(pagedData);

            var result = new PagedResult<ShipEquipmentInfoDto>(ShipEquipmentInfoDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }

        private static IQueryable<T> ApplyOrdering<T>(IQueryable<T> source, string propertyName, bool descending)
        {
            var entityType = typeof(T);
            var parameter = Expression.Parameter(entityType, "x");

            // Mapping UI keys to actual Entity Framework property paths
            var propertyMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "SchoolName", "BaseSchoolName.SchoolName" },
        { "EquipmentCategory", "EquipmentCategory.Name" },
        { "EqupmentName", "EqupmentName.Name" },
        { "AcquisitionMethodName", "AcquisitionMethod.Name" },
        { "StateOfEquipment", "StateOfEquipment.Name" },
        { "LastModificationDate", "LastModifiedDate" }
    };

            if (propertyMappings.ContainsKey(propertyName))
            {
                propertyName = propertyMappings[propertyName]; // Convert UI key to actual entity property
            }

            try
            {
                // Handle nested properties (e.g., "BaseSchoolName.SchoolName")
                Expression propertyExpression = parameter;
                foreach (var member in propertyName.Split('.'))
                {
                    propertyExpression = Expression.PropertyOrField(propertyExpression, member);
                }

                var lambda = Expression.Lambda(propertyExpression, parameter);
                string methodName = descending ? "OrderByDescending" : "OrderBy";

                var resultExpression = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new Type[] { entityType, propertyExpression.Type },
                    source.Expression,
                    Expression.Quote(lambda)
                );

                return source.Provider.CreateQuery<T>(resultExpression);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Sorting error: Property '{propertyName}' not found or invalid", ex);
            }
        }


    }
}

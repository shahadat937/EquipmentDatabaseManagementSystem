using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetShipEquipmentByCategoryIdAndOplNonOplQtyRequestHandler : IRequestHandler<GetShipEquipmentByCategoryIdAndOplNonOplQtyRequest, PagedResult<ShipEquipmentInfoDto>>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _shipEquipmentRepository;
        private readonly IMapper _mapper;
        public GetShipEquipmentByCategoryIdAndOplNonOplQtyRequestHandler(ISchoolManagementRepository<ShipEquipmentInfo> shipEquipmentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _shipEquipmentRepository = shipEquipmentRepository;

        }
        public async Task<PagedResult<ShipEquipmentInfoDto>> Handle(GetShipEquipmentByCategoryIdAndOplNonOplQtyRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            // Base query with conditional filtering
            IQueryable<ShipEquipmentInfo> query = _shipEquipmentRepository.FilterWithInclude(
                x => x.EquipmentCategoryId == request.CategoryId  && (request.IsOpl? x.OplQty > 0 : x.NonOplQty > 0) &&
                     (x.Model.Contains(request.QueryParams.SearchText) || x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText) || x.EqupmentName.Name.Contains((request.QueryParams.SearchText)) || string.IsNullOrEmpty(request.QueryParams.SearchText)),
                "EquipmentCategory", "EqupmentName", "StateOfEquipment", "BaseSchoolName", "AcquisitionMethod");

            // Total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination
            var pagedData = await query
                .OrderByDescending(x => x.ShipEquipmentInfoId)
                .Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
                .Take(request.QueryParams.PageSize)
                .ToListAsync(cancellationToken);

            

            // Map to DTOs
            var resultDtos = _mapper.Map<List<ShipEquipmentInfoDto>>(pagedData);

            // Return paginated result
            return new PagedResult<ShipEquipmentInfoDto>(resultDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);
        }

      


    }


}

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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetShipEquipmentInfoByCategoryIdEquipmentStatusIdAndCommandingAreaIdRequestHandelar : IRequestHandler<GetShipEquipmentInfoByCategoryIdEquipmentStatusIdAndCommandingAreaIdRequest, object>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _shipEquipmentRepository;
        private readonly ISchoolManagementRepository<ShipInformation> _shipRepository;
        private readonly ISchoolManagementRepository<BaseSchoolName> _baseRepository;
        private readonly IMapper _mapper;

        public GetShipEquipmentInfoByCategoryIdEquipmentStatusIdAndCommandingAreaIdRequestHandelar(ISchoolManagementRepository<ShipEquipmentInfo> shipEquipmentRepository, IMapper mapper, ISchoolManagementRepository<ShipInformation> shipRepository, ISchoolManagementRepository<BaseSchoolName> baseRepository)
        {
            _mapper = mapper;
            _shipEquipmentRepository = shipEquipmentRepository;
            _shipRepository = shipRepository;
            _baseRepository = baseRepository;
        }

        public async Task<object> Handle(GetShipEquipmentInfoByCategoryIdEquipmentStatusIdAndCommandingAreaIdRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var spQuery = String.Format("exec [GetShipEquipmentInfoByCategoryIdAndStateOfEquipmentId] {0}, {1}, {2}, {3}, {4}, {5}, {6}",
           
            string.IsNullOrEmpty(request.QueryParams.SearchText) ? "NULL" : $"'{request.QueryParams.SearchText}'",
            request.QueryParams.PageSize,
            request.QueryParams.PageNumber,
            request.CategoryId,
            request.StateOfEquipmentId,
            request.CommandingAreaId);
            DataTable dataTable = _shipEquipmentRepository.ExecWithSqlQuery(spQuery);

            int totalCount = 0;
            if (dataTable.Rows.Count > 0)
            {
                totalCount = Convert.ToInt32(dataTable.Rows[0]["TotalCount"]);
            }

            return dataTable;


        }
    }

}
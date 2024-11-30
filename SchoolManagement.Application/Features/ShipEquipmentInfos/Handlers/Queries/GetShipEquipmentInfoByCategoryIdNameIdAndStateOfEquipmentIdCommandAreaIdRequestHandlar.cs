using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Queries
{
    public class GetShipEquipmentInfoByCategoryIdNameIdAndStateOfEquipmentIdCommandAreaIdRequestHandlar : IRequestHandler<GetShipEquipmentInfoByCategoryIdNameIdAndStateOfEquipmentIdCommandAreaIdRequest, object>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _shipEquipmentRepository;
        private readonly IMapper _mapper;

        public GetShipEquipmentInfoByCategoryIdNameIdAndStateOfEquipmentIdCommandAreaIdRequestHandlar(ISchoolManagementRepository<ShipEquipmentInfo> shipEquipmentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _shipEquipmentRepository = shipEquipmentRepository;
        }
        public async Task<object> Handle(GetShipEquipmentInfoByCategoryIdNameIdAndStateOfEquipmentIdCommandAreaIdRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var spQuery = String.Format("exec [getShipEquipmentByCategoryIdNameIdAndStateOfEquipmentStatus] {0}, {1}, {2}, {3}, {4}, {5}, {6}",

            string.IsNullOrEmpty(request.QueryParams.SearchText) ? "''" : $"'{request.QueryParams.SearchText}'",
            request.QueryParams.PageSize,
            request.QueryParams.PageNumber,
            request.CategoryId,
            request.StateOfEquipmentId,
            request.CommandingAreaId,
            request.EquipmentNameId);
            DataTable dataTable = _shipEquipmentRepository.ExecWithSqlQuery(spQuery);



            return dataTable;
        }
    }
}

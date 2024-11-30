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
    public class GetShipEquipmentInfoByAuthorityIdListRequestHandlar : IRequestHandler<GetShipEquipmentInfoByAuthorityIdListRequest, object>
    {
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _shipEquipmentRepository;
        private readonly IMapper _mapper;

        public GetShipEquipmentInfoByAuthorityIdListRequestHandlar(ISchoolManagementRepository<ShipEquipmentInfo> shipEquipmentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _shipEquipmentRepository = shipEquipmentRepository;
        }
        public async Task<object> Handle(GetShipEquipmentInfoByAuthorityIdListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var spQuery = String.Format("exec [spgetShipEquipmentByAuthority] {0}, {1}, {2}, {3}",

            string.IsNullOrEmpty(request.QueryParams.SearchText) ? "''" : $"'{request.QueryParams.SearchText}'",
            request.QueryParams.PageSize,
            request.QueryParams.PageNumber,
            request.AuthorityId);
            DataTable dataTable = _shipEquipmentRepository.ExecWithSqlQuery(spQuery);

            return dataTable;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem;
using SchoolManagement.Application.DTOs.PaymentStatus;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Handlers.Queries
{
    public class GetOperationalStatusOfEquipmentSystemListCommandHandlar : IRequestHandler<GetOperationalStatusOfEquipmentSystemListCommand, PagedResult<OperationalStatusOfEquipmentSystemDto>>
    {
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem> _OperationalStatusOfEquipmentSystem;

        private readonly IMapper _mapper;
        public GetOperationalStatusOfEquipmentSystemListCommandHandlar(ISchoolManagementRepository<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem> PaymentStatusRepository, IMapper mapper)
        {
            _OperationalStatusOfEquipmentSystem = PaymentStatusRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<OperationalStatusOfEquipmentSystemDto>> Handle(GetOperationalStatusOfEquipmentSystemListCommand request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem> PaymentStatuss = _OperationalStatusOfEquipmentSystem.FilterWithInclude(x => (x.BaseSchoolName.SchoolName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "EqupmentName", "OperationalStatus", "BaseSchoolName");
            var totalCount = PaymentStatuss.Count();
            PaymentStatuss = PaymentStatuss.OrderByDescending(x => x.OperationalStatusOfEquipmentSystemId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var PaymentStatusDtos = _mapper.Map<List<OperationalStatusOfEquipmentSystemDto>>(PaymentStatuss);
            var result = new PagedResult<OperationalStatusOfEquipmentSystemDto>(PaymentStatusDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }
    }
}

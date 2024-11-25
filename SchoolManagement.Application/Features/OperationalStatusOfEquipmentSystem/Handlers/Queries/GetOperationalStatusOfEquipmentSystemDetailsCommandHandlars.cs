using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem;
using SchoolManagement.Application.DTOs.PaymentStatus;
using SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Handlers.Queries
{
    public class GetOperationalStatusOfEquipmentSystemDetailsCommandHandlars : IRequestHandler<GetOperationalStatusOfEquipmentSystemDetailsCommand, OperationalStatusOfEquipmentSystemDto>
    {
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem> _OperationalStatusOfEquipmentSystem;
        private readonly IMapper _mapper;
        public GetOperationalStatusOfEquipmentSystemDetailsCommandHandlars(ISchoolManagementRepository<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem> PaymentStatusRepository, IMapper mapper)
        {
            _OperationalStatusOfEquipmentSystem = PaymentStatusRepository;
            _mapper = mapper;
        }
        public async Task<OperationalStatusOfEquipmentSystemDto> Handle(GetOperationalStatusOfEquipmentSystemDetailsCommand request, CancellationToken cancellationToken)
        {
            var result= await _OperationalStatusOfEquipmentSystem.Get(request.OperationalStatusOfEquipmentSystemId);
            return _mapper.Map<OperationalStatusOfEquipmentSystemDto>(result);
        }
    }
}

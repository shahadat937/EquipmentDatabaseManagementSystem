using MediatR;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Commands
{
    public class UpdateOperationalStatusOfEquipmentSystemCommand: IRequest<Unit>
    {
        public CreateOperationalStatusOfEquipmentSystemDto OperationalStatusOfEquipmentSystemDto { get; set; }
    }
}

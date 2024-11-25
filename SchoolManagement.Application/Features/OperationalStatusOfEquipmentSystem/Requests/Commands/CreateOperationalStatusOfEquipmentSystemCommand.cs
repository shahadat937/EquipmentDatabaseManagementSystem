using MediatR;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Commands
{
    public class CreateOperationalStatusOfEquipmentSystemCommand: IRequest<BaseCommandResponse>
    {
        public CreateOperationalStatusOfEquipmentSystemDto OperationalStatusOfEquipmentSystemDto { get; set; }
    }
}

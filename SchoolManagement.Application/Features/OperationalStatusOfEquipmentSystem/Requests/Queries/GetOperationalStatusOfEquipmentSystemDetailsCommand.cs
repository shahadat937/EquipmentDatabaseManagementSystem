using MediatR;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Queries
{
    public class GetOperationalStatusOfEquipmentSystemDetailsCommand : IRequest<OperationalStatusOfEquipmentSystemDto>
    {
        public int OperationalStatusOfEquipmentSystemId { get; set; }
    }
}

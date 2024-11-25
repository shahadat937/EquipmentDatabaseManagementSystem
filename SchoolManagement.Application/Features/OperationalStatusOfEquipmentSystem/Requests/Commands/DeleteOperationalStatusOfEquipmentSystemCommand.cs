using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Commands
{
    public class DeleteOperationalStatusOfEquipmentSystemCommand : IRequest
    {
        public int OperationalStatusOfEquipmentSystemId { get; set; }
    }
}

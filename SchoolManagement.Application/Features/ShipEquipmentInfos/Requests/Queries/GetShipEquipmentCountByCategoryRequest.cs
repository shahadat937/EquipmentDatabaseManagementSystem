using MediatR;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries
{
    public class GetShipEquipmentCountByCategoryRequest: IRequest<object>
    {
        public int StateOfEquipmentId1 { get; set; }
        public int StateOfEquipmentId2 { get; set; }
    }
}

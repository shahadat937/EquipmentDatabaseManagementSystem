using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries
{
    public class GetCompatSystemEequipmentCountRequest : IRequest<object>
    {
        public int CombatSystemId { get; set; }
        public int StateOfEquipmentId1 { get; set; }
        public int StateOfEquipmentId2 { get; set; }
    }
}

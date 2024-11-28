using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Queries
{
    public class GetShipEquipmentInfoByCategoryIdEquipmentStatusIdAndCommandingAreaIdRequest : IRequest<object>
    {
        public QueryParams QueryParams { get; set; }
        public int CategoryId { get; set; }
        public int StateOfEquipmentId { get; set; }
        public int CommandingAreaId { get; set; }
    }
}

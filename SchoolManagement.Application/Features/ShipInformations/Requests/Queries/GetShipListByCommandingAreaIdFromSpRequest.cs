using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ShipInformations.Requests.Queries
{
    public class GetShipListByCommandingAreaIdFromSpRequest : IRequest<object>
    {
        public int ShipTypeId { get; set; }
        public int CommandingAreaId { get; set; }
    }
}

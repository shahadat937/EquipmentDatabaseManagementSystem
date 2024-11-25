using MediatR;
using SchoolManagement.Application.DTOs.StatusOfShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StatusOfShip.Request.Queries
{
    public class GetStatusOfShipDetailsRequest :IRequest<StatusOfShipDto>
    {
        public int StatusOfShipId { get; set; }
    }
}

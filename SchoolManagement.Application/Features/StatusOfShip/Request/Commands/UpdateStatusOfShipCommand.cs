using MediatR;
using SchoolManagement.Application.DTOs.StatusOfShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StatusOfShip.Request.Commands
{
    public class UpdateStatusOfShipCommand:IRequest
    {
        public StatusOfShipDto StatusOfShipDto { get; set; }
    }
}

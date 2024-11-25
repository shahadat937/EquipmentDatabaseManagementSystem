using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StatusOfShip.Request.Commands
{
    public class DeleteStatusOfShipCommand:IRequest
    {
        public int StatusOfShipId { get; set; }
    }
}

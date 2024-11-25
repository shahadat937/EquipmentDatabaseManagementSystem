using MediatR;
using SchoolManagement.Application.DTOs.StatusOfShip;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StatusOfShip.Request.Commands
{
    public class CreateStatusOfShipCommand : IRequest<BaseCommandResponse>
    {
        public CreateStatusOfShipDto StatusOfShipDto { get; set; }
    }
}

using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Queries
{
    public class GetOperationalStatusOfEquipmentSystemListCommand: IRequest<PagedResult<OperationalStatusOfEquipmentSystemDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}

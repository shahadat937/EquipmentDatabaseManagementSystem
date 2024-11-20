using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Procurements.Requests.Queries
{
    public class GetProcurementListByProcurementMethodIdRequest : IRequest <PagedResult<ProcurementDto>>
    {
        public QueryParams QueryParams { get; set; }
        public string? SearchBy { get; set; }
        public int ProcureMethodId { get; set; }
    }
}

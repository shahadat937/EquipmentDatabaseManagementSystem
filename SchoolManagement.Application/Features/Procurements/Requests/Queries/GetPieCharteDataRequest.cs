using MediatR;
using SchoolManagement.Application.DTOs.Procurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Procurements.Requests.Queries
{
    public class GetPieCharteDataRequest : IRequest<List<PieChartDataDto>>
    {
    }
}

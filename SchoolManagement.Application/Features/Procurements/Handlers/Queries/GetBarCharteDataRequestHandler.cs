using MediatR;
using SchoolManagement.Application.DTOs.Procurement;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetBarCharteDataRequestHandler : IRequestHandler<GetBarCharteDataRequest, ProcurementDataForBarChartDto>
    {
        public Task<ProcurementDataForBarChartDto> Handle(GetBarCharteDataRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using MediatR;
using SchoolManagement.Application.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Project.Requests.Queries
{
    public class GetProjectBarChartDataRequest : IRequest<ProjectBarChartDto>
    {
    }
}

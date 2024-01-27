using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReportTypes.Requests.Queries
{
    public class GetSelectedReportTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 

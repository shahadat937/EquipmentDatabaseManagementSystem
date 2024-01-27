using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReportingMonths.Requests.Queries
{
    public class GetSelectedReportingMonthRequest : IRequest<List<SelectedModel>>
    {
    }
} 

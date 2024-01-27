using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Queries
{
    public class GetSelectedBookUserManualBRInfoRequest : IRequest<List<SelectedModel>>
    {
    }
} 

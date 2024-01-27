using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Handlers.Queries
{
    public class GetSelectedBookUserManualBRInfoRequestHandler : IRequestHandler<GetSelectedBookUserManualBRInfoRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<BookUserManualBRInfo> _BookUserManualBRInfoRepository;


        public GetSelectedBookUserManualBRInfoRequestHandler(ISchoolManagementRepository<BookUserManualBRInfo> BookUserManualBRInfoRepository)
        {
            _BookUserManualBRInfoRepository = BookUserManualBRInfoRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBookUserManualBRInfoRequest request, CancellationToken cancellationToken)
        {
            ICollection<BookUserManualBRInfo> codeValues = await _BookUserManualBRInfoRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.BookName,
                Value = x.BookUserManualBRInfoId
            }).ToList();
            return selectModels;
        }
    }
}

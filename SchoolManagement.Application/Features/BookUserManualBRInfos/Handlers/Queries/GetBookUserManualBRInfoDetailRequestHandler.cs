using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;
using SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Handlers.Queries
{
    public class GetBookUserManualBRInfoDetailRequestHandler : IRequestHandler<GetBookUserManualBRInfoDetailRequest, BookUserManualBRInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<BookUserManualBRInfo> _BookUserManualBRInfoRepository;
        public GetBookUserManualBRInfoDetailRequestHandler(ISchoolManagementRepository<BookUserManualBRInfo> BookUserManualBRInfoRepository, IMapper mapper)
        {
            _BookUserManualBRInfoRepository = BookUserManualBRInfoRepository;
            _mapper = mapper;
        }
        public async Task<BookUserManualBRInfoDto> Handle(GetBookUserManualBRInfoDetailRequest request, CancellationToken cancellationToken)
        {
            var BookUserManualBRInfo = await _BookUserManualBRInfoRepository.Get(request.BookUserManualBRInfoId);
            return _mapper.Map<BookUserManualBRInfoDto>(BookUserManualBRInfo);
        }
    }
}

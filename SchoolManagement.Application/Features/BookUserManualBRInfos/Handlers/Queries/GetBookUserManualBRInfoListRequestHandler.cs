using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo;
using SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Handlers.Queries
{
    public class GetBookUserManualBRInfoListRequestHandler : IRequestHandler<GetBookUserManualBRInfoListRequest, PagedResult<BookUserManualBRInfoDto>>
    {

        private readonly ISchoolManagementRepository<BookUserManualBRInfo> _BookUserManualBRInfoRepository;

        private readonly IMapper _mapper;

        public GetBookUserManualBRInfoListRequestHandler(ISchoolManagementRepository<BookUserManualBRInfo> BookUserManualBRInfoRepository, IMapper mapper)
        {
            _BookUserManualBRInfoRepository = BookUserManualBRInfoRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BookUserManualBRInfoDto>> Handle(GetBookUserManualBRInfoListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<BookUserManualBRInfo> BookUserManualBRInfos = _BookUserManualBRInfoRepository.FilterWithInclude(x => (x.BookName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "BaseSchoolName", "BookType");
            var totalCount = BookUserManualBRInfos.Count();
            BookUserManualBRInfos = BookUserManualBRInfos.OrderByDescending(x => x.BookUserManualBRInfoId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var BookUserManualBRInfoDtos = _mapper.Map<List<BookUserManualBRInfoDto>>(BookUserManualBRInfos);
            var result = new PagedResult<BookUserManualBRInfoDto>(BookUserManualBRInfoDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

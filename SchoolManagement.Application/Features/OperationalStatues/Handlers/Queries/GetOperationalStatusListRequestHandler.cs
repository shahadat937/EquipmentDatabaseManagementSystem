using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.OperationalStatuss;
using SchoolManagement.Application.Features.OperationalStatuss.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.OperationalStatuss.Handlers.Queries
{
    public class GetOperationalStatusListRequestHandler : IRequestHandler<GetOperationalStatusListRequest, PagedResult<OperationalStatusDto>>
    {

        private readonly ISchoolManagementRepository<OperationalStatus> _OperationalStatusRepository;

        private readonly IMapper _mapper;

        public GetOperationalStatusListRequestHandler(ISchoolManagementRepository<OperationalStatus> OperationalStatusRepository, IMapper mapper)
        {
            _OperationalStatusRepository = OperationalStatusRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<OperationalStatusDto>> Handle(GetOperationalStatusListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<OperationalStatus> OperationalStatuss = _OperationalStatusRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = OperationalStatuss.Count();
            OperationalStatuss = OperationalStatuss.OrderByDescending(x => x.OperationalStatusId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var OperationalStatusDtos = _mapper.Map<List<OperationalStatusDto>>(OperationalStatuss);
            var result = new PagedResult<OperationalStatusDto>(OperationalStatusDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

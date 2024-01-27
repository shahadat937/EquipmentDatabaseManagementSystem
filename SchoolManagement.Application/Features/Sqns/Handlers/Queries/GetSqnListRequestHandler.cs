using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Sqns;
using SchoolManagement.Application.Features.Sqns.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Sqns.Handlers.Queries
{
    public class GetSqnListRequestHandler : IRequestHandler<GetSqnListRequest, PagedResult<SqnDto>>
    {

        private readonly ISchoolManagementRepository<Sqn> _SqnRepository;

        private readonly IMapper _mapper;

        public GetSqnListRequestHandler(ISchoolManagementRepository<Sqn> SqnRepository, IMapper mapper)
        {
            _SqnRepository = SqnRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SqnDto>> Handle(GetSqnListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Sqn> Sqns = _SqnRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Sqns.Count();
            Sqns = Sqns.OrderByDescending(x => x.SqnId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var SqnDtos = _mapper.Map<List<SqnDto>>(Sqns);
            var result = new PagedResult<SqnDto>(SqnDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

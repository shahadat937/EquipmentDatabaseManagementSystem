using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.LetterTypes;
using SchoolManagement.Application.Features.LetterTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.LetterTypes.Handlers.Queries
{
    public class GetLetterTypeListRequestHandler : IRequestHandler<GetLetterTypeListRequest, PagedResult<LetterTypeDto>>
    {

        private readonly ISchoolManagementRepository<LetterType> _LetterTypeRepository;

        private readonly IMapper _mapper;

        public GetLetterTypeListRequestHandler(ISchoolManagementRepository<LetterType> LetterTypeRepository, IMapper mapper)
        {
            _LetterTypeRepository = LetterTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<LetterTypeDto>> Handle(GetLetterTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<LetterType> LetterTypes = _LetterTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = LetterTypes.Count();
            LetterTypes = LetterTypes.OrderByDescending(x => x.LetterTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var LetterTypeDtos = _mapper.Map<List<LetterTypeDto>>(LetterTypes);
            var result = new PagedResult<LetterTypeDto>(LetterTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

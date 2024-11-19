using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.BookType;
using SchoolManagement.Application.Features.BookTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.BookTypes.Handlers.Queries
{
    public class GetBookTypeListRequestHandler : IRequestHandler<GetBookTypeListRequest, PagedResult<BookTypeDto>>
    {

        private readonly ISchoolManagementRepository<BookType> _BookTypeRepository;

        private readonly IMapper _mapper;

        public GetBookTypeListRequestHandler(ISchoolManagementRepository<BookType> BookTypeRepository, IMapper mapper)
        {
            _BookTypeRepository = BookTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<BookTypeDto>> Handle(GetBookTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<BookType> BookTypes = _BookTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || (x.Remarks.Contains(request.QueryParams.SearchText)) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = BookTypes.Count();
            BookTypes = BookTypes.OrderBy(x => x.MenuPosition).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var BookTypeDtos = _mapper.Map<List<BookTypeDto>>(BookTypes);
            var result = new PagedResult<BookTypeDto>(BookTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

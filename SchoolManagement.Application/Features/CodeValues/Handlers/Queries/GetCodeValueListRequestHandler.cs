using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.CodeValues;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System.ComponentModel.DataAnnotations;
using SchoolManagement.Application.Features.CodeValues.Requests.Queries;

namespace SchoolManagement.Application.Features.CodeValues.Handlers.Queries
{
    public class GetCodeValueListRequestHandler : IRequestHandler<GetCodeValueListRequest, PagedResult<CodeValueDto>>
    { 

        private readonly ISchoolManagementRepository<CodeValue> _CodeValueRepository;    

        private readonly IMapper _mapper;  
         
        public GetCodeValueListRequestHandler(ISchoolManagementRepository<CodeValue> CodeValueRepository, IMapper mapper)
        {
            _CodeValueRepository = CodeValueRepository; 
            _mapper = mapper;  
        }

        public async Task<PagedResult<CodeValueDto>> Handle(GetCodeValueListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString()); 

            IQueryable<CodeValue> CodeValues = _CodeValueRepository.FilterWithInclude(x => (x.Code.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "CodeValueType");
            var totalCount = CodeValues.Count();
            CodeValues = CodeValues.OrderByDescending(x => x.CodeValueId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
              
            var CodeValuesDtos = _mapper.Map<List<CodeValueDto>>(CodeValues); 
            var result = new PagedResult<CodeValueDto>(CodeValuesDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }
    }
}

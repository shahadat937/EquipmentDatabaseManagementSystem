using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.CodeValueType;
using SchoolManagement.Application.Features.CodeValueTypes.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.CodeValueTypes.Handlers.Queries 
{ 
    public class GetCodeValueTypeListRequestHandler : IRequestHandler<GetCodeValueTypeListRequest, PagedResult<CodeValueTypeDto>>
    { 

        private readonly ISchoolManagementRepository<CodeValueType> _CodeValueTypeRepository;    

        private readonly IMapper _mapper;  
         
        public GetCodeValueTypeListRequestHandler(ISchoolManagementRepository<CodeValueType> CodeValueTypeRepository, IMapper mapper)
        {
            _CodeValueTypeRepository = CodeValueTypeRepository; 
            _mapper = mapper;  
        }

        public async Task<PagedResult<CodeValueTypeDto>> Handle(GetCodeValueTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString()); 

            IQueryable<CodeValueType> CodeValueTypes = _CodeValueTypeRepository.FilterWithInclude(x => (x.Type.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = CodeValueTypes.Count();
            CodeValueTypes = CodeValueTypes.OrderByDescending(x => x.CodeValueTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
              
            var CodeValueTypesDtos = _mapper.Map<List<CodeValueTypeDto>>(CodeValueTypes); 
            var result = new PagedResult<CodeValueTypeDto>(CodeValueTypesDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }
    }
}

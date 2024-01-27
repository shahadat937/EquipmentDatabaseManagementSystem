using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.BaseSchoolNames;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SchoolManagement.Application.Features.BaseSchoolNames.Requests.Queries;

namespace SchoolManagement.Application.Features.BaseSchoolNames.Handlers.Queries 
{ 
    public class GetBaseSchoolNameListRequestHandler : IRequestHandler<GetBaseSchoolNameListRequest, PagedResult<BaseSchoolNameDto>>
    { 

        private readonly ISchoolManagementRepository<BaseSchoolName> _BaseSchoolNameRepository;    

        private readonly IMapper _mapper;  
         
        public GetBaseSchoolNameListRequestHandler(ISchoolManagementRepository<BaseSchoolName> BaseSchoolNameRepository, IMapper mapper)
        {
            _BaseSchoolNameRepository = BaseSchoolNameRepository; 
            _mapper = mapper;  
        }

        public async Task<PagedResult<BaseSchoolNameDto>> Handle(GetBaseSchoolNameListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString()); 

            IQueryable<BaseSchoolName> BaseSchoolNames = _BaseSchoolNameRepository.FilterWithInclude(x => (x.SchoolName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "BaseName");
            var totalCount = BaseSchoolNames.Count();
            BaseSchoolNames = BaseSchoolNames.OrderBy(x => x.BaseSchoolNameId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
              
            var BaseSchoolNamesDtos = _mapper.Map<List<BaseSchoolNameDto>>(BaseSchoolNames); 
            var result = new PagedResult<BaseSchoolNameDto>(BaseSchoolNamesDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        } 
    }
}

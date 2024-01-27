using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.Modules;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SchoolManagement.Application.Features.Modules.Requests.Queries;

namespace SchoolManagement.Application.Features.Modules.Handlers.Queries
{
    public class GetModuleListRequestHandler : IRequestHandler<GetModuleListRequest, PagedResult<ModuleDto>>
    { 

        private readonly ISchoolManagementRepository<Module> _ModuleRepository;    

        private readonly IMapper _mapper;  
         
        public GetModuleListRequestHandler(ISchoolManagementRepository<Module> ModuleRepository, IMapper mapper)
        {
            _ModuleRepository = ModuleRepository; 
            _mapper = mapper;  
        }

        public async Task<PagedResult<ModuleDto>> Handle(GetModuleListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString()); 

            IQueryable<Module> Modules = _ModuleRepository.FilterWithInclude(x => (x.Title.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Modules.Count();
            Modules = Modules.OrderBy(x => x.ModuleId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
              
            var ModulesDtos = _mapper.Map<List<ModuleDto>>(Modules); 
            var result = new PagedResult<ModuleDto>(ModulesDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }
    }
}

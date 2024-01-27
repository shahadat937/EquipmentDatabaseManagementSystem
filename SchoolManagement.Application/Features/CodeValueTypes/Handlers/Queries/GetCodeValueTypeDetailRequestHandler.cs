using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CodeValueType;
using SchoolManagement.Application.Features.CodeValueTypes.Requests.Queries;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.CodeValueTypes.Handlers.Queries
{  
    public class GetCodeValueTypeDetailRequestHandler : IRequestHandler<GetCodeValueTypeDetailRequest, CodeValueTypeDto>
    {
        private readonly IMapper _mapper;   
        private readonly ISchoolManagementRepository<CodeValueType> _CodeValueTypeRepository;       
        public GetCodeValueTypeDetailRequestHandler(ISchoolManagementRepository<CodeValueType> CodeValueTypeRepository, IMapper mapper)
        {
            _CodeValueTypeRepository = CodeValueTypeRepository;    
            _mapper = mapper; 
        } 
        public async Task<CodeValueTypeDto> Handle(GetCodeValueTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var CodeValueType = await _CodeValueTypeRepository.Get(request.Id);    
            return _mapper.Map<CodeValueTypeDto>(CodeValueType);    
        }
    }
}

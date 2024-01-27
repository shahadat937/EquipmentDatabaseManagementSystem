using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CodeValueType.Validators;
using SchoolManagement.Application.Features.CodeValueTypes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.CodeValueTypes.Handlers.Commands   
{
    public class CreateCodeValueTypeCommandHandler : IRequestHandler<CreateCodeValueTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
         
        public CreateCodeValueTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateCodeValueTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCodeValueTypeDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.CodeValueTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var CodeValueType = _mapper.Map<CodeValueType>(request.CodeValueTypeDto); 

                CodeValueType = await _unitOfWork.Repository<CodeValueType>().Add(CodeValueType);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful"; 
                response.Id = CodeValueType.CodeValueTypeId;
            }

            return response;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CodeValueType.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.CodeValueTypes.Requests.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.CodeValueTypes.Handlers.Commands
{  
    public class UpdateCodeValueTypeCommandHandler : IRequestHandler<UpdateCodeValueTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public UpdateCodeValueTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(UpdateCodeValueTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCodeValueTypeDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.CodeValueTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult); 
             
            var CodeValueType = await _unitOfWork.Repository<CodeValueType>().Get(request.CodeValueTypeDto.CodeValueTypeId); 

            if (CodeValueType is null)  
                throw new NotFoundException(nameof(CodeValueType), request.CodeValueTypeDto.CodeValueTypeId); 

            _mapper.Map(request.CodeValueTypeDto, CodeValueType);  

            await _unitOfWork.Repository<CodeValueType>().Update(CodeValueType); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
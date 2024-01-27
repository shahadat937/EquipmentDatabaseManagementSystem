using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CodeValues.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.CodeValues.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CodeValues.Handlers.Commands
{
    public class UpdateCodeValueCommandHandler : IRequestHandler<UpdateCodeValueCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public UpdateCodeValueCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(UpdateCodeValueCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCodeValueDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.CodeValueDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult); 
             
            var CodeValue = await _unitOfWork.Repository<CodeValue>().Get(request.CodeValueDto.CodeValueId); 

            if (CodeValue is null)  
                throw new NotFoundException(nameof(CodeValue), request.CodeValueDto.CodeValueId); 

            _mapper.Map(request.CodeValueDto, CodeValue);  

            await _unitOfWork.Repository<CodeValue>().Update(CodeValue); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
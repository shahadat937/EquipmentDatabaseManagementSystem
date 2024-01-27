using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReturnTypes.Validators;
using SchoolManagement.Application.Features.ReturnTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.ReturnTypes.Handlers.Commands
{
    public class UpdateReturnTypeCommandHandler : IRequestHandler<UpdateReturnTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReturnTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReturnTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateReturnTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ReturnTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ReturnType = await _unitOfWork.Repository<ReturnType>().Get(request.ReturnTypeDto.ReturnTypeId);

            if (ReturnType is null)
                throw new NotFoundException(nameof(ReturnType), request.ReturnTypeDto.ReturnTypeId);

            _mapper.Map(request.ReturnTypeDto, ReturnType);

            await _unitOfWork.Repository<ReturnType>().Update(ReturnType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.LetterTypes.Validators;
using SchoolManagement.Application.Features.LetterTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.LetterTypes.Handlers.Commands
{
    public class UpdateLetterTypeCommandHandler : IRequestHandler<UpdateLetterTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLetterTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLetterTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLetterTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.LetterTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var LetterType = await _unitOfWork.Repository<LetterType>().Get(request.LetterTypeDto.LetterTypeId);

            if (LetterType is null)
                throw new NotFoundException(nameof(LetterType), request.LetterTypeDto.LetterTypeId);

            _mapper.Map(request.LetterTypeDto, LetterType);

            await _unitOfWork.Repository<LetterType>().Update(LetterType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

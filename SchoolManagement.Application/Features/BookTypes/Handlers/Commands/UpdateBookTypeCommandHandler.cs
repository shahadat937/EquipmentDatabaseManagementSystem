using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BookType.Validators;
using SchoolManagement.Application.Features.BookTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.BookTypes.Handlers.Commands
{
    public class UpdateBookTypeCommandHandler : IRequestHandler<UpdateBookTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBookTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBookTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBookTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.BookTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var BookType = await _unitOfWork.Repository<BookType>().Get(request.BookTypeDto.BookTypeId);

            if (BookType is null)
                throw new NotFoundException(nameof(BookType), request.BookTypeDto.BookTypeId);

            _mapper.Map(request.BookTypeDto, BookType);

            await _unitOfWork.Repository<BookType>().Update(BookType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

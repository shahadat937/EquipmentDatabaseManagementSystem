using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BookUserManualBRInfo.Validators;
using SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Commands;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Handlers.Commands
{
    public class UpdateBookUserManualBRInfoCommandHandler : IRequestHandler<UpdateBookUserManualBRInfoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBookUserManualBRInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBookUserManualBRInfoCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBookUserManualBRInfoDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.BookUserManualBRInfoDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var BookUserManualBRInfo = await _unitOfWork.Repository<BookUserManualBRInfo>().Get(request.BookUserManualBRInfoDto.BookUserManualBRInfoId);

            if (BookUserManualBRInfo is null)
                throw new NotFoundException(nameof(BookUserManualBRInfo), request.BookUserManualBRInfoDto.BookUserManualBRInfoId);

            _mapper.Map(request.BookUserManualBRInfoDto, BookUserManualBRInfo);

            await _unitOfWork.Repository<BookUserManualBRInfo>().Update(BookUserManualBRInfo);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

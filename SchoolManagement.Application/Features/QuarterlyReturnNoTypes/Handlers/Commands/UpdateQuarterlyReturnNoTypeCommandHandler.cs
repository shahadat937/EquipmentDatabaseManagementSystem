using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes.Validators;
using SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Handlers.Commands
{
    public class UpdateQuarterlyReturnNoTypeCommandHandler : IRequestHandler<UpdateQuarterlyReturnNoTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateQuarterlyReturnNoTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateQuarterlyReturnNoTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateQuarterlyReturnNoTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.QuarterlyReturnNoTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var QuarterlyReturnNoType = await _unitOfWork.Repository<QuarterlyReturnNoType>().Get(request.QuarterlyReturnNoTypeDto.QuarterlyReturnNoTypeId);

            if (QuarterlyReturnNoType is null)
                throw new NotFoundException(nameof(QuarterlyReturnNoType), request.QuarterlyReturnNoTypeDto.QuarterlyReturnNoTypeId);

            _mapper.Map(request.QuarterlyReturnNoTypeDto, QuarterlyReturnNoType);

            await _unitOfWork.Repository<QuarterlyReturnNoType>().Update(QuarterlyReturnNoType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.TenderOpeningDateTypes.Validators;
using SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Handlers.Commands
{
    public class UpdateTenderOpeningDateTypeCommandHandler : IRequestHandler<UpdateTenderOpeningDateTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTenderOpeningDateTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTenderOpeningDateTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTenderOpeningDateTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.TenderOpeningDateTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var TenderOpeningDateType = await _unitOfWork.Repository<TenderOpeningDateType>().Get(request.TenderOpeningDateTypeDto.TenderOpeningDateTypeId);

            if (TenderOpeningDateType is null)
                throw new NotFoundException(nameof(TenderOpeningDateType), request.TenderOpeningDateTypeDto.TenderOpeningDateTypeId);

            _mapper.Map(request.TenderOpeningDateTypeDto, TenderOpeningDateType);

            await _unitOfWork.Repository<TenderOpeningDateType>().Update(TenderOpeningDateType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

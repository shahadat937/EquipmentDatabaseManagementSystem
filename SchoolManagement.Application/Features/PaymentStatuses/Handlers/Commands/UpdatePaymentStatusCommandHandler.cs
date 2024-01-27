using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PaymentStatus.Validators;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Commands;

namespace SchoolManagement.Application.Features.PaymentStatuss.Handlers.Commands
{
    public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePaymentStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePaymentStatusDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.PaymentStatusDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var PaymentStatus = await _unitOfWork.Repository<PaymentStatus>().Get(request.PaymentStatusDto.PaymentStatusId);

            if (PaymentStatus is null)
                throw new NotFoundException(nameof(PaymentStatus), request.PaymentStatusDto.PaymentStatusId);

            _mapper.Map(request.PaymentStatusDto, PaymentStatus);

            await _unitOfWork.Repository<PaymentStatus>().Update(PaymentStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PaymentStatus.Validators;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.PaymentStatuss.Handlers.Commands
{
    public class CreatePaymentStatusCommandHandler : IRequestHandler<CreatePaymentStatusCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePaymentStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePaymentStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PaymentStatusDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var PaymentStatus = _mapper.Map<PaymentStatus>(request.PaymentStatusDto);

                PaymentStatus = await _unitOfWork.Repository<PaymentStatus>().Add(PaymentStatus);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = PaymentStatus.PaymentStatusId;
            }

            return response;
        }
    }
}

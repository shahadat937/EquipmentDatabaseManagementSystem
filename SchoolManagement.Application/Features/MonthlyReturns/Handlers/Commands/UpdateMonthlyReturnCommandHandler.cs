using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands;

namespace SchoolManagement.Application.Features.MonthlyReturns.Handlers.Commands
{
    public class UpdateMonthlyReturnCommandHandler : IRequestHandler<UpdateMonthlyReturnCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMonthlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMonthlyReturnCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMonthlyReturnDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.MonthlyReturnDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var MonthlyReturn = await _unitOfWork.Repository<MonthlyReturn>().Get(request.MonthlyReturnDto.MonthlyReturnId);

            if (MonthlyReturn is null)
                throw new NotFoundException(nameof(MonthlyReturn), request.MonthlyReturnDto.MonthlyReturnId);

            _mapper.Map(request.MonthlyReturnDto, MonthlyReturn);

            await _unitOfWork.Repository<MonthlyReturn>().Update(MonthlyReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

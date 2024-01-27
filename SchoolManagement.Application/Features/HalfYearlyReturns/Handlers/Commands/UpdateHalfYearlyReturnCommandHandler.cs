using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.HalfYearlyReturn.Validators;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Commands;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Handlers.Commands
{
    public class UpdateHalfYearlyReturnCommandHandler : IRequestHandler<UpdateHalfYearlyReturnCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHalfYearlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateHalfYearlyReturnCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateHalfYearlyReturnDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.HalfYearlyReturnDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var HalfYearlyReturn = await _unitOfWork.Repository<HalfYearlyReturn>().Get(request.HalfYearlyReturnDto.HalfYearlyReturnId);

            if (HalfYearlyReturn is null)
                throw new NotFoundException(nameof(HalfYearlyReturn), request.HalfYearlyReturnDto.HalfYearlyReturnId);

            _mapper.Map(request.HalfYearlyReturnDto, HalfYearlyReturn);

            await _unitOfWork.Repository<HalfYearlyReturn>().Update(HalfYearlyReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

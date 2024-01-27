using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime.Validators;
using SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Commands;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Handlers.Commands
{
    public class UpdateHalfYearlyRunningTimeCommandHandler : IRequestHandler<UpdateHalfYearlyRunningTimeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHalfYearlyRunningTimeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateHalfYearlyRunningTimeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateHalfYearlyRunningTimeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.HalfYearlyRunningTimeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var HalfYearlyRunningTime = await _unitOfWork.Repository<HalfYearlyRunningTime>().Get(request.HalfYearlyRunningTimeDto.HalfYearlyRunningTimeId);

            if (HalfYearlyRunningTime is null)
                throw new NotFoundException(nameof(HalfYearlyRunningTime), request.HalfYearlyRunningTimeDto.HalfYearlyRunningTimeId);

            _mapper.Map(request.HalfYearlyRunningTimeDto, HalfYearlyRunningTime);

            await _unitOfWork.Repository<HalfYearlyRunningTime>().Update(HalfYearlyRunningTime);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

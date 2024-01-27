using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RunningTimes.Validators;
using SchoolManagement.Application.Features.RunningTimes.Requests.Commands;

namespace SchoolManagement.Application.Features.RunningTimes.Handlers.Commands
{
    public class UpdateRunningTimeCommandHandler : IRequestHandler<UpdateRunningTimeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRunningTimeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateRunningTimeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateRunningTimeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.RunningTimeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var RunningTime = await _unitOfWork.Repository<RunningTime>().Get(request.RunningTimeDto.RunningTimeId);

            if (RunningTime is null)
                throw new NotFoundException(nameof(RunningTime), request.RunningTimeDto.RunningTimeId);

            _mapper.Map(request.RunningTimeDto, RunningTime);

            await _unitOfWork.Repository<RunningTime>().Update(RunningTime);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

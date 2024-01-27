using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.RunningTimes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.RunningTimes.Handlers.Commands
{
    public class DeleteRunningTimeCommandHandler : IRequestHandler<DeleteRunningTimeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRunningTimeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteRunningTimeCommand request, CancellationToken cancellationToken)
        {
            var RunningTime = await _unitOfWork.Repository<RunningTime>().Get(request.RunningTimeId);

            if (RunningTime == null)
                throw new NotFoundException(nameof(RunningTime), request.RunningTimeId);

            await _unitOfWork.Repository<RunningTime>().Delete(RunningTime);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

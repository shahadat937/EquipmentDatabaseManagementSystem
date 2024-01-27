using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Handlers.Commands
{
    public class DeleteHalfYearlyRunningTimeCommandHandler : IRequestHandler<DeleteHalfYearlyRunningTimeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHalfYearlyRunningTimeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteHalfYearlyRunningTimeCommand request, CancellationToken cancellationToken)
        {
            var HalfYearlyRunningTime = await _unitOfWork.Repository<HalfYearlyRunningTime>().Get(request.HalfYearlyRunningTimeId);

            if (HalfYearlyRunningTime == null)
                throw new NotFoundException(nameof(HalfYearlyRunningTime), request.HalfYearlyRunningTimeId);

            await _unitOfWork.Repository<HalfYearlyRunningTime>().Delete(HalfYearlyRunningTime);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

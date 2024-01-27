using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DailyWorkStates.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyWorkStates.Handlers.Commands
{
    public class DeleteDailyWorkStateCommandHandler : IRequestHandler<DeleteDailyWorkStateCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDailyWorkStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDailyWorkStateCommand request, CancellationToken cancellationToken)
        {
            var DailyWorkState = await _unitOfWork.Repository<DailyWorkState>().Get(request.DailyWorkStateId);

            if (DailyWorkState == null)
                throw new NotFoundException(nameof(DailyWorkState), request.DailyWorkStateId);

            await _unitOfWork.Repository<DailyWorkState>().Delete(DailyWorkState);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

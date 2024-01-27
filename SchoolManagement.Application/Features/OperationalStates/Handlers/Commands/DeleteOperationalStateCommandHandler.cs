using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.OperationalStates.Requests.Commands;
using SchoolManagement.Doamin;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.OperationalStates.Handlers.Commands
{
    public class DeleteOperationalStateCommandHandler : IRequestHandler<DeleteOperationalStateCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOperationalStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOperationalStateCommand request, CancellationToken cancellationToken)
        {
            var OperationalState = await _unitOfWork.Repository<OperationalState>().Get(request.OperationalStateId);

            if (OperationalState == null)
                throw new NotFoundException(nameof(OperationalState), request.OperationalStateId);

            await _unitOfWork.Repository<OperationalState>().Delete(OperationalState);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

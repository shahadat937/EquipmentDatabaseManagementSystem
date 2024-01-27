using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ActionTakens.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ActionTakens.Handlers.Commands
{
    public class DeleteActionTakenCommandHandler : IRequestHandler<DeleteActionTakenCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteActionTakenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteActionTakenCommand request, CancellationToken cancellationToken)
        {
            var ActionTaken = await _unitOfWork.Repository<ActionTaken>().Get(request.ActionTakenId);

            if (ActionTaken == null)
                throw new NotFoundException(nameof(ActionTaken), request.ActionTakenId);

            await _unitOfWork.Repository<ActionTaken>().Delete(ActionTaken);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Prioritys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Prioritys.Handlers.Commands
{
    public class DeletePriorityCommandHandler : IRequestHandler<DeletePriorityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePriorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeletePriorityCommand request, CancellationToken cancellationToken)
        {
            var Priority = await _unitOfWork.Repository<Priority>().Get(request.PriorityId);

            if (Priority == null)
                throw new NotFoundException(nameof(Priority), request.PriorityId);

            await _unitOfWork.Repository<Priority>().Delete(Priority);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

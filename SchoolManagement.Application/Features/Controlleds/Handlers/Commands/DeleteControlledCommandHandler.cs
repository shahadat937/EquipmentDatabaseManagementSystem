using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Controlleds.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Controlleds.Handlers.Commands
{
    public class DeleteControlledCommandHandler : IRequestHandler<DeleteControlledCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteControlledCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteControlledCommand request, CancellationToken cancellationToken)
        {
            var Controlled = await _unitOfWork.Repository<Controlled>().Get(request.ControlledId);

            if (Controlled == null)
                throw new NotFoundException(nameof(Controlled), request.ControlledId);

            await _unitOfWork.Repository<Controlled>().Delete(Controlled);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

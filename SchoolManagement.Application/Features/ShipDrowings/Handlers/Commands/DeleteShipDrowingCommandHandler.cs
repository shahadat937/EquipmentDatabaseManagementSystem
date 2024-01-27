using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShipDrowings.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipDrowings.Handlers.Commands
{
    public class DeleteShipDrowingCommandHandler : IRequestHandler<DeleteShipDrowingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShipDrowingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteShipDrowingCommand request, CancellationToken cancellationToken)
        {
            var ShipDrowing = await _unitOfWork.Repository<ShipDrowing>().Get(request.ShipDrowingId);

            if (ShipDrowing == null)
                throw new NotFoundException(nameof(ShipDrowing), request.ShipDrowingId);

            await _unitOfWork.Repository<ShipDrowing>().Delete(ShipDrowing);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

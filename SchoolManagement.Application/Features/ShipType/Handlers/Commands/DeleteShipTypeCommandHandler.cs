using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShipTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipTypes.Handlers.Commands
{
    public class DeleteShipTypeCommandHandler : IRequestHandler<DeleteShipTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShipTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteShipTypeCommand request, CancellationToken cancellationToken)
        {
            var ShipType = await _unitOfWork.Repository<ShipType>().Get(request.ShipTypeId);

            if (ShipType == null)
                throw new NotFoundException(nameof(ShipType), request.ShipTypeId);

            await _unitOfWork.Repository<ShipType>().Delete(ShipType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

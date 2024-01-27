using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Commands
{
    public class DeleteShipEquipmentInfoCommandHandler : IRequestHandler<DeleteShipEquipmentInfoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShipEquipmentInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteShipEquipmentInfoCommand request, CancellationToken cancellationToken)
        {
            var ShipEquipmentInfo = await _unitOfWork.Repository<ShipEquipmentInfo>().Get(request.ShipEquipmentInfoId);

            if (ShipEquipmentInfo == null)
                throw new NotFoundException(nameof(ShipEquipmentInfo), request.ShipEquipmentInfoId);

            await _unitOfWork.Repository<ShipEquipmentInfo>().Delete(ShipEquipmentInfo);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

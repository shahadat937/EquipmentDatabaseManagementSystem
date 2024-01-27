using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo.Validators;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Commands;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Commands
{
    public class UpdateShipEquipmentInfoCommandHandler : IRequestHandler<UpdateShipEquipmentInfoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShipEquipmentInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateShipEquipmentInfoCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateShipEquipmentInfoDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ShipEquipmentInfoDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ShipEquipmentInfo = await _unitOfWork.Repository<ShipEquipmentInfo>().Get(request.ShipEquipmentInfoDto.ShipEquipmentInfoId);

            if (ShipEquipmentInfo is null)
                throw new NotFoundException(nameof(ShipEquipmentInfo), request.ShipEquipmentInfoDto.ShipEquipmentInfoId);

            _mapper.Map(request.ShipEquipmentInfoDto, ShipEquipmentInfo);

            await _unitOfWork.Repository<ShipEquipmentInfo>().Update(ShipEquipmentInfo);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

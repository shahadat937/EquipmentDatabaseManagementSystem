using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShipTypes.Requests.Commands;
using SchoolManagement.Application.DTOs.ShipTypeDtos.Validators;

namespace SchoolManagement.Application.Features.ShipTypes.Handlers.Commands
{
    public class UpdateShipTypeCommandHandler : IRequestHandler<UpdateShipTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShipTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateShipTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateShipTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ShipTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ShipType = await _unitOfWork.Repository<ShipType>().Get(request.ShipTypeDto.ShipTypeId);

            if (ShipType is null)
                throw new NotFoundException(nameof(ShipType), request.ShipTypeDto.ShipTypeId);

            _mapper.Map(request.ShipTypeDto, ShipType);

            await _unitOfWork.Repository<ShipType>().Update(ShipType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

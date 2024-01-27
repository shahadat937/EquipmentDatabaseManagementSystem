using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipInformations.Validators;
using SchoolManagement.Application.Features.ShipInformations.Requests.Commands;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Commands
{
    public class UpdateShipInformationCommandHandler : IRequestHandler<UpdateShipInformationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShipInformationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateShipInformationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateShipInformationDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ShipInformationDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ShipInformation = await _unitOfWork.Repository<ShipInformation>().Get(request.ShipInformationDto.ShipInformationId);

            if (ShipInformation is null)
                throw new NotFoundException(nameof(ShipInformation), request.ShipInformationDto.ShipInformationId);

            _mapper.Map(request.ShipInformationDto, ShipInformation);

            await _unitOfWork.Repository<ShipInformation>().Update(ShipInformation);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

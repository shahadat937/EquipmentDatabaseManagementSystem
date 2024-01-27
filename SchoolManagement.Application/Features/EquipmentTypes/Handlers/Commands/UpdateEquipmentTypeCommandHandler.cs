using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EquipmentType.Validators;
using SchoolManagement.Application.Features.EquipmentTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.EquipmentTypes.Handlers.Commands
{
    public class UpdateEquipmentTypeCommandHandler : IRequestHandler<UpdateEquipmentTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEquipmentTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEquipmentTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEquipmentTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.EquipmentTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var EquipmentType = await _unitOfWork.Repository<EquipmentType>().Get(request.EquipmentTypeDto.EquipmentTypeId);

            if (EquipmentType is null)
                throw new NotFoundException(nameof(EquipmentType), request.EquipmentTypeDto.EquipmentTypeId);

            _mapper.Map(request.EquipmentTypeDto, EquipmentType);

            await _unitOfWork.Repository<EquipmentType>().Update(EquipmentType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

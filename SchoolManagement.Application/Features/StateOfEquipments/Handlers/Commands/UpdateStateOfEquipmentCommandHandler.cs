using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.StateOfEquipments.Validators;
using SchoolManagement.Application.Features.StateOfEquipments.Requests.Commands;

namespace SchoolManagement.Application.Features.StateOfEquipments.Handlers.Commands
{
    public class UpdateStateOfEquipmentCommandHandler : IRequestHandler<UpdateStateOfEquipmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStateOfEquipmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateStateOfEquipmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateStateOfEquipmentDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.StateOfEquipmentDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var StateOfEquipment = await _unitOfWork.Repository<StateOfEquipment>().Get(request.StateOfEquipmentDto.StateOfEquipmentId);

            if (StateOfEquipment is null)
                throw new NotFoundException(nameof(StateOfEquipment), request.StateOfEquipmentDto.StateOfEquipmentId);

            _mapper.Map(request.StateOfEquipmentDto, StateOfEquipment);

            await _unitOfWork.Repository<StateOfEquipment>().Update(StateOfEquipment);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

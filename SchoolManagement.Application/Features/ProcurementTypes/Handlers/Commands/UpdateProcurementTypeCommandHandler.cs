using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProcurementType.Validators;
using SchoolManagement.Application.Features.ProcurementTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.ProcurementTypes.Handlers.Commands
{
    public class UpdateProcurementTypeCommandHandler : IRequestHandler<UpdateProcurementTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProcurementTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProcurementTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProcurementTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ProcurementTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ProcurementType = await _unitOfWork.Repository<ProcurementType>().Get(request.ProcurementTypeDto.ProcurementTypeId);

            if (ProcurementType is null)
                throw new NotFoundException(nameof(ProcurementType), request.ProcurementTypeDto.ProcurementTypeId);

            _mapper.Map(request.ProcurementTypeDto, ProcurementType);

            await _unitOfWork.Repository<ProcurementType>().Update(ProcurementType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

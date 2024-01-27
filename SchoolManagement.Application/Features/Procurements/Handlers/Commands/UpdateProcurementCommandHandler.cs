using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Procurement.Validators;
using SchoolManagement.Application.Features.Procurements.Requests.Commands;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Commands
{
    public class UpdateProcurementCommandHandler : IRequestHandler<UpdateProcurementCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProcurementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProcurementCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProcurementDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ProcurementDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Procurement = await _unitOfWork.Repository<Procurement>().Get(request.ProcurementDto.ProcurementId);

            if (Procurement is null)
                throw new NotFoundException(nameof(Procurement), request.ProcurementDto.ProcurementId);

            _mapper.Map(request.ProcurementDto, Procurement);

            await _unitOfWork.Repository<Procurement>().Update(Procurement);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

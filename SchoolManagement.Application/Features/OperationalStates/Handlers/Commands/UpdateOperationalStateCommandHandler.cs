using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.OperationalStates.Requests.Commands;
using SchoolManagement.Application.DTOs.OperationalStates.Validators;
using SchoolManagement.Doamin;

namespace SchoolManagement.Application.Features.OperationalStates.Handlers.Commands
{
    public class UpdateOperationalStateCommandHandler : IRequestHandler<UpdateOperationalStateCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOperationalStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOperationalStateCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOperationalStateDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.OperationalStateDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var OperationalState = await _unitOfWork.Repository<OperationalState>().Get(request.OperationalStateDto.OperationalStateId);

            if (OperationalState is null)
                throw new NotFoundException(nameof(OperationalState), request.OperationalStateDto.OperationalStateId);

            _mapper.Map(request.OperationalStateDto, OperationalState);

            await _unitOfWork.Repository<OperationalState>().Update(OperationalState);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

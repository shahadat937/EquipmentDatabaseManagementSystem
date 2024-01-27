using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ActionTaken.Validators;
using SchoolManagement.Application.Features.ActionTakens.Requests.Commands;

namespace SchoolManagement.Application.Features.ActionTakens.Handlers.Commands
{
    public class UpdateActionTakenCommandHandler : IRequestHandler<UpdateActionTakenCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateActionTakenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateActionTakenCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateActionTakenDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ActionTakenDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ActionTaken = await _unitOfWork.Repository<ActionTaken>().Get(request.ActionTakenDto.ActionTakenId);

            if (ActionTaken is null)
                throw new NotFoundException(nameof(ActionTaken), request.ActionTakenDto.ActionTakenId);

            _mapper.Map(request.ActionTakenDto, ActionTaken);

            await _unitOfWork.Repository<ActionTaken>().Update(ActionTaken);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

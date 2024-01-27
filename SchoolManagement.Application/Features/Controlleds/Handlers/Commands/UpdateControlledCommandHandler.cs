using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Controlled.Validators;
using SchoolManagement.Application.Features.Controlleds.Requests.Commands;

namespace SchoolManagement.Application.Features.Controlleds.Handlers.Commands
{
    public class UpdateControlledCommandHandler : IRequestHandler<UpdateControlledCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateControlledCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateControlledCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateControlledDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ControlledDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Controlled = await _unitOfWork.Repository<Controlled>().Get(request.ControlledDto.ControlledId);

            if (Controlled is null)
                throw new NotFoundException(nameof(Controlled), request.ControlledDto.ControlledId);

            _mapper.Map(request.ControlledDto, Controlled);

            await _unitOfWork.Repository<Controlled>().Update(Controlled);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

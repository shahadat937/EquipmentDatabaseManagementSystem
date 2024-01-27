using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Priority.Validators;
using SchoolManagement.Application.Features.Prioritys.Requests.Commands;

namespace SchoolManagement.Application.Features.Prioritys.Handlers.Commands
{
    public class UpdatePriorityCommandHandler : IRequestHandler<UpdatePriorityCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePriorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePriorityDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.PriorityDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Priority = await _unitOfWork.Repository<Priority>().Get(request.PriorityDto.PriorityId);

            if (Priority is null)
                throw new NotFoundException(nameof(Priority), request.PriorityDto.PriorityId);

            _mapper.Map(request.PriorityDto, Priority);

            await _unitOfWork.Repository<Priority>().Update(Priority);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

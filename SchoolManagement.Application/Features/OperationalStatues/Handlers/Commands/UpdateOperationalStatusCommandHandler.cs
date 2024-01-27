using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.OperationalStatuss.Validators;
using SchoolManagement.Application.Features.OperationalStatuss.Requests.Commands;

namespace SchoolManagement.Application.Features.OperationalStatuss.Handlers.Commands
{
    public class UpdateOperationalStatusCommandHandler : IRequestHandler<UpdateOperationalStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOperationalStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOperationalStatusCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOperationalStatusDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.OperationalStatusDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var OperationalStatus = await _unitOfWork.Repository<OperationalStatus>().Get(request.OperationalStatusDto.OperationalStatusId);

            if (OperationalStatus is null)
                throw new NotFoundException(nameof(OperationalStatus), request.OperationalStatusDto.OperationalStatusId);

            _mapper.Map(request.OperationalStatusDto, OperationalStatus);

            await _unitOfWork.Repository<OperationalStatus>().Update(OperationalStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

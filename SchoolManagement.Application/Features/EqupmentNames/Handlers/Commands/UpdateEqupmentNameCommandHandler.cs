using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EqupmentNames.Validators;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Commands;

namespace SchoolManagement.Application.Features.EqupmentNames.Handlers.Commands
{
    public class UpdateEqupmentNameCommandHandler : IRequestHandler<UpdateEqupmentNameCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEqupmentNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEqupmentNameCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEqupmentNameDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.EqupmentNameDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var EqupmentName = await _unitOfWork.Repository<EqupmentName>().Get(request.EqupmentNameDto.EqupmentNameId);

            if (EqupmentName is null)
                throw new NotFoundException(nameof(EqupmentName), request.EqupmentNameDto.EqupmentNameId);

            _mapper.Map(request.EqupmentNameDto, EqupmentName);

            await _unitOfWork.Repository<EqupmentName>().Update(EqupmentName);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

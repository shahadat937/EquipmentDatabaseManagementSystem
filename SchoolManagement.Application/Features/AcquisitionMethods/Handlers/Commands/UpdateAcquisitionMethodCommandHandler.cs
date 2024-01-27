using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.AcquisitionMethod.Validators;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Commands;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Handlers.Commands
{
    public class UpdateAcquisitionMethodCommandHandler : IRequestHandler<UpdateAcquisitionMethodCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAcquisitionMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAcquisitionMethodCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateAcquisitionMethodDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.AcquisitionMethodDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var AcquisitionMethod = await _unitOfWork.Repository<AcquisitionMethod>().Get(request.AcquisitionMethodDto.AcquisitionMethodId);

            if (AcquisitionMethod is null)
                throw new NotFoundException(nameof(AcquisitionMethod), request.AcquisitionMethodDto.AcquisitionMethodId);

            _mapper.Map(request.AcquisitionMethodDto, AcquisitionMethod);

            await _unitOfWork.Repository<AcquisitionMethod>().Update(AcquisitionMethod);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

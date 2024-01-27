using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProcurementMethod.Validators;
using SchoolManagement.Application.Features.ProcurementMethods.Requests.Commands;

namespace SchoolManagement.Application.Features.ProcurementMethods.Handlers.Commands
{
    public class UpdateProcurementMethodCommandHandler : IRequestHandler<UpdateProcurementMethodCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProcurementMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProcurementMethodCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProcurementMethodDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ProcurementMethodDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ProcurementMethod = await _unitOfWork.Repository<ProcurementMethod>().Get(request.ProcurementMethodDto.ProcurementMethodId);

            if (ProcurementMethod is null)
                throw new NotFoundException(nameof(ProcurementMethod), request.ProcurementMethodDto.ProcurementMethodId);

            _mapper.Map(request.ProcurementMethodDto, ProcurementMethod);

            await _unitOfWork.Repository<ProcurementMethod>().Update(ProcurementMethod);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

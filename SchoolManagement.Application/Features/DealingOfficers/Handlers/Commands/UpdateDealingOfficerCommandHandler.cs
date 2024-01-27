using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DealingOfficer.Validators;
using SchoolManagement.Application.Features.DealingOfficers.Requests.Commands;

namespace SchoolManagement.Application.Features.DealingOfficers.Handlers.Commands
{
    public class UpdateDealingOfficerCommandHandler : IRequestHandler<UpdateDealingOfficerCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDealingOfficerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDealingOfficerCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDealingOfficerDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.DealingOfficerDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var DealingOfficer = await _unitOfWork.Repository<DealingOfficer>().Get(request.DealingOfficerDto.DealingOfficerId);

            if (DealingOfficer is null)
                throw new NotFoundException(nameof(DealingOfficer), request.DealingOfficerDto.DealingOfficerId);

            _mapper.Map(request.DealingOfficerDto, DealingOfficer);

            await _unitOfWork.Repository<DealingOfficer>().Update(DealingOfficer);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

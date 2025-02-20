using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.YearSetup.Validators;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Commands
{
    public class UpdateYearSetupCommandHandler : IRequestHandler<UpdateYearSetupCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateYearSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateYearSetupCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateYearSetupDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.YearSetupDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var YearSetup = await _unitOfWork.Repository<YearSetup>().Get(request.YearSetupDto.YearId);

            if (YearSetup is null)
                throw new NotFoundException(nameof(YearSetup), request.YearSetupDto.YearId);

            _mapper.Map(request.YearSetupDto, YearSetup);

            await _unitOfWork.Repository<YearSetup>().Update(YearSetup);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

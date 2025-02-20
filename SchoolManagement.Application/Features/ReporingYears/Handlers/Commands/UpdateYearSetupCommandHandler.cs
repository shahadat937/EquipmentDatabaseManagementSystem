using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;
using SchoolManagement.Application.Features.ReporingYears.Requests.Commands;
using SchoolManagement.Application.DTOs.ReporingYear.Validators;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Commands
{
    public class UpdateYearSetupCommandHandler : IRequestHandler<UpdateReportingYearCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateYearSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReportingYearCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateReportingYearDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ReporingYearDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var YearSetup = await _unitOfWork.Repository<ReportingYear>().Get(request.ReporingYearDto.ReportingYearId);

            if (YearSetup is null)
                throw new NotFoundException(nameof(YearSetup), request.ReporingYearDto.ReportingYearId);

            _mapper.Map(request.ReporingYearDto, YearSetup);

            await _unitOfWork.Repository<ReportingYear>().Update(YearSetup);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

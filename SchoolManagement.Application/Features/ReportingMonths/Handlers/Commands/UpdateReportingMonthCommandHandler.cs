using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ReportingMonths.Requests.Commands;
using SchoolManagement.Application.DTOs.ReportingMonths.Validators;
using SchoolManagement.Doamin;

namespace SchoolManagement.Application.Features.ReportingMonths.Handlers.Commands
{
    public class UpdateReportingMonthCommandHandler : IRequestHandler<UpdateReportingMonthCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReportingMonthCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReportingMonthCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateReportingMonthDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ReportingMonthDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ReportingMonth = await _unitOfWork.Repository<ReportingMonth>().Get(request.ReportingMonthDto.ReportingMonthId);

            if (ReportingMonth is null)
                throw new NotFoundException(nameof(ReportingMonth), request.ReportingMonthDto.ReportingMonthId);

            _mapper.Map(request.ReportingMonthDto, ReportingMonth);

            await _unitOfWork.Repository<ReportingMonth>().Update(ReportingMonth);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

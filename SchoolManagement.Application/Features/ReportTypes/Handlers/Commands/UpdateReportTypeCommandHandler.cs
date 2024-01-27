using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReportType.Validators;
using SchoolManagement.Application.Features.ReportTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.ReportTypes.Handlers.Commands
{
    public class UpdateReportTypeCommandHandler : IRequestHandler<UpdateReportTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReportTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReportTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateReportTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ReportTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ReportType = await _unitOfWork.Repository<ReportType>().Get(request.ReportTypeDto.ReportTypeId);

            if (ReportType is null)
                throw new NotFoundException(nameof(ReportType), request.ReportTypeDto.ReportTypeId);

            _mapper.Map(request.ReportTypeDto, ReportType);

            await _unitOfWork.Repository<ReportType>().Update(ReportType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

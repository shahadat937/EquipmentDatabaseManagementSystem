using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReportingMonths.Validators;
using SchoolManagement.Application.Features.ReportingMonths.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Doamin;

namespace SchoolManagement.Application.Features.ReportingMonths.Handlers.Commands
{
    public class CreateReportingMonthCommandHandler : IRequestHandler<CreateReportingMonthCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReportingMonthCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateReportingMonthCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateReportingMonthDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ReportingMonthDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ReportingMonth = _mapper.Map<ReportingMonth>(request.ReportingMonthDto);

                ReportingMonth = await _unitOfWork.Repository<ReportingMonth>().Add(ReportingMonth);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ReportingMonth.ReportingMonthId;
            }

            return response;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReportingYear.Validators;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Commands
{
    public class CreateYearSetupCommandHandler : IRequestHandler<CreateReportingYearCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateYearSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateReportingYearCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateReportingYearDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ReportingYearDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var YearSetup = _mapper.Map<ReportingYear>(request.ReportingYearDto);

                YearSetup = await _unitOfWork.Repository<ReportingYear>().Add(YearSetup);

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
                response.Id = YearSetup.ReportingYearId;
            }

            return response;
        }
    }
}

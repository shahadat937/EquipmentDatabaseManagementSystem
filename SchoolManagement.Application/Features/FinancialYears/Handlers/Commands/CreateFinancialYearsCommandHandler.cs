using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FinancialYears.Validators;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FinancialYearss.Handlers.Commands
{
    public class CreateFinancialYearsCommandHandler : IRequestHandler<CreateFinancialYearsCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFinancialYearsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFinancialYearsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFinancialYearDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FinancialYearsDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var FinancialYears = _mapper.Map<SchoolManagement.Domain.FinancialYear>(request.FinancialYearsDto);

                FinancialYears = await _unitOfWork.Repository<SchoolManagement.Domain.FinancialYear>().Add(FinancialYears);

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
                response.Id = FinancialYears.FinancialYearId;
            }

            return response;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.DTOs.YearlyReturns.Validators;
using SchoolManagement.Application.Features.YearlyReturns.Request.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Handler.Commands
{
    public class CreateYearlyReturnCommandHandler : IRequestHandler<CreateYearlyReturnCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateYearlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateYearlyReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            // Validation
            var validator = new CreateYearlyReturnDtoValidator();
            var validationResult = await validator.ValidateAsync(request.YearlyReturnDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                // Mapping DTO to Entity
                var yearlyReturn = _mapper.Map<YearlyReturn>(request.YearlyReturnDto);

                // Adding the entity to the repository
                yearlyReturn = await _unitOfWork.Repository<YearlyReturn>().Add(yearlyReturn);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    // Handle exception
                    response.Success = false;
                    response.Message = "Error occurred while saving.";
                    response.Errors = new List<string> { ex.Message };
                    return response;
                }

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = yearlyReturn.YearlyReturnId;
            }

            return response;
        }
    }

}

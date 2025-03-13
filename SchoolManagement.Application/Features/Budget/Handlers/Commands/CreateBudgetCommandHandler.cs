using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Budget.Validators;
using SchoolManagement.Application.Features.Budget.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Budget.Handlers.Commands
{
    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBudgetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBudgetDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BudgetDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            try
            {
                var Budget = _mapper.Map<SchoolManagement.Domain.Budget>(request.BudgetDto);
                Budget = await _unitOfWork.Repository<SchoolManagement.Domain.Budget>().Add(Budget);
                await _unitOfWork.Save();

                if (request.BudgetDto.BaseSchoolNameId != null && request.BudgetDto.BaseSchoolNameId.Any())
                {
                    var baseSchoolNames = request.BudgetDto.BaseSchoolNameId
                        .Select(baseSchoolId => new BudgetBaseSchoolName
                        {
                            BudgetId = Budget.BudgetId,
                            BaseSchoolNameId = baseSchoolId 
                        }).ToList();

                    await _unitOfWork.Repository<BudgetBaseSchoolName>().AddRangeAsync(baseSchoolNames);
                    await _unitOfWork.Save();
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Budget.BudgetId;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                response.Success = false;
                response.Message = "An error occurred while creating Budget.";
            }

            return response;
        }

    }
}
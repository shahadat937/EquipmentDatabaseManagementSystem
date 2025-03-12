using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ReporingYears.Requests.Commands;
using SchoolManagement.Application.DTOs.ReporingYear.Validators;
using SchoolManagement.Application.DTOs.BudgetCode.Validators;
using SchoolManagement.Application.Features.BudgetCode.Requests.Commands;
using SchoolManagement.Application.DTOs.BudgetCodes.Validators;

namespace SchoolManagement.Application.Features.BudgetCodes.Handlers.Commands
{
    public class UpdateBudgetCodeCommandHandler : IRequestHandler<UpdateBudgetCodeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBudgetCodeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBudgetCodeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBudgetCodeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.BudgetCode);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var BudgetCode = await _unitOfWork.Repository<SchoolManagement.Domain.BudgetCode>().Get(request.BudgetCode.BudgetCodeId);

            if (BudgetCode is null)
                throw new NotFoundException(nameof(BudgetCode), request.BudgetCode.BudgetCodeId);

            _mapper.Map(request.BudgetCode, BudgetCode);

            await _unitOfWork.Repository<SchoolManagement.Domain.BudgetCode>().Update(BudgetCode);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

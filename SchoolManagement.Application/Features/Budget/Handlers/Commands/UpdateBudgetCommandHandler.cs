using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Budget.Validators;
using SchoolManagement.Application.Features.Budget.Requests.Commands;

namespace SchoolManagement.Application.Features.Budget.Handlers.Commands
{
    public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<BudgetBaseSchoolName> _procuremenetBaseSchoolNameRepository;
       

        public UpdateBudgetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISchoolManagementRepository<BudgetBaseSchoolName> procuremenetBaseSchoolNameRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _procuremenetBaseSchoolNameRepository = procuremenetBaseSchoolNameRepository;

        }

        public async Task<Unit> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBudgetDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.BudgetDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Budget = await _unitOfWork.Repository<SchoolManagement.Domain.Budget>().Get(request.BudgetDto.BudgetId);

            if (Budget is null)
                throw new NotFoundException(nameof(Budget), request.BudgetDto.BudgetId);

            _mapper.Map(request.BudgetDto, Budget);

            await _unitOfWork.Repository<SchoolManagement.Domain.Budget>().Update(Budget);
            await _unitOfWork.Save();

            var BudgetBaseSchool = _procuremenetBaseSchoolNameRepository.FilterWithInclude(x => x.BudgetId == request.BudgetDto.BudgetId);

           
            await _procuremenetBaseSchoolNameRepository.RemoveRangeAsync(BudgetBaseSchool);
          

            if (request.BudgetDto.BaseSchoolNameId != null && request.BudgetDto.BaseSchoolNameId.Any())
            {
                var baseSchoolNames = request.BudgetDto.BaseSchoolNameId
                    .Select(baseSchoolId => new BudgetBaseSchoolName
                    {
                        BudgetId = request.BudgetDto.BudgetId,
                        BaseSchoolNameId = baseSchoolId
                    }).ToList();

                await _unitOfWork.Repository<BudgetBaseSchoolName>().AddRangeAsync(baseSchoolNames);
                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}

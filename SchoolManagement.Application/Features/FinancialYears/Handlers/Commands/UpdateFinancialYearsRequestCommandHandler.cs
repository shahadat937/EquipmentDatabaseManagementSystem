using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ReporingYears.Requests.Commands;
using SchoolManagement.Application.DTOs.ReporingYear.Validators;
using SchoolManagement.Application.DTOs.FinancialYears.Validators;

namespace SchoolManagement.Application.Features.FinancialYearss.Handlers.Commands
{
    public class UpdateFinancialYearsCommandHandler : IRequestHandler<UpdateFinancialYearsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFinancialYearsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFinancialYearsCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFinancialYearsDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ReporingYearDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var FinancialYears = await _unitOfWork.Repository<SchoolManagement.Domain.FinancialYears>().Get(request.ReporingYearDto.FinancialYearId);

            if (FinancialYears is null)
                throw new NotFoundException(nameof(FinancialYears), request.ReporingYearDto.FinancialYearId);

            _mapper.Map(request.ReporingYearDto, FinancialYears);

            await _unitOfWork.Repository<SchoolManagement.Domain.FinancialYears>().Update(FinancialYears);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

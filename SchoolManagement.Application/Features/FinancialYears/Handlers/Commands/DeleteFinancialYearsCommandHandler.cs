using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;

namespace SchoolManagement.Application.Features.FinancialYearss.Handlers.Commands
{
    public class DeleteFinancialYearsCommandHandler : IRequestHandler<DeleteFinancialYearsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFinancialYearsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFinancialYearsCommand request, CancellationToken cancellationToken)
        {
            var FinancialYears = await _unitOfWork.Repository<SchoolManagement.Domain.FinancialYear>().Get(request.FinancialYearId);

            if (FinancialYears == null)
                throw new NotFoundException(nameof(FinancialYears), request.FinancialYearId);

            await _unitOfWork.Repository<SchoolManagement.Domain.FinancialYear>().Delete(FinancialYears);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

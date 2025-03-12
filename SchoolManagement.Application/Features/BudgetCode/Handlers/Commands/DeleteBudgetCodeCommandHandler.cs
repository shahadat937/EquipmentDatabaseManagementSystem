using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.BudgetCode.Requests.Commands;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;

namespace SchoolManagement.Application.Features.BudgetCodes.Handlers.Commands
{
    public class DeleteBudgetCodeCommandHandler : IRequestHandler<DeleteBudgetCodeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBudgetCodeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBudgetCodeCommand request, CancellationToken cancellationToken)
        {
            var BudgetCode = await _unitOfWork.Repository<SchoolManagement.Domain.BudgetCode>().Get(request.BudgetCodeId);

            if (BudgetCode == null)
                throw new NotFoundException(nameof(BudgetCode), request.BudgetCodeId);

            await _unitOfWork.Repository<SchoolManagement.Domain.BudgetCode>().Delete(BudgetCode);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

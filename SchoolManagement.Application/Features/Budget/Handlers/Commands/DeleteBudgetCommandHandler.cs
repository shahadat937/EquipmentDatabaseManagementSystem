using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Budget.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Budget.Handlers.Commands
{
    public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBudgetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
        {
            var Budget = await _unitOfWork.Repository<SchoolManagement.Domain.Budget>().Get(request.BudgetId);

            if (Budget == null)
                throw new NotFoundException(nameof(Budget), request.BudgetId);

            await _unitOfWork.Repository<SchoolManagement.Domain.Budget>().Delete(Budget);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

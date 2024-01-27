using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.MonthlyReturns.Handlers.Commands
{
    public class DeleteMonthlyReturnCommandHandler : IRequestHandler<DeleteMonthlyReturnCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteMonthlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteMonthlyReturnCommand request, CancellationToken cancellationToken)
        {
            var MonthlyReturn = await _unitOfWork.Repository<MonthlyReturn>().Get(request.MonthlyReturnId);

            if (MonthlyReturn == null)
                throw new NotFoundException(nameof(MonthlyReturn), request.MonthlyReturnId);

            await _unitOfWork.Repository<MonthlyReturn>().Delete(MonthlyReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

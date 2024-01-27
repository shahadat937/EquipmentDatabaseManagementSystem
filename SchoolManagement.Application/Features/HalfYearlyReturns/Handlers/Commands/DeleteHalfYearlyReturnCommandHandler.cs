using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Handlers.Commands
{
    public class DeleteHalfYearlyReturnCommandHandler : IRequestHandler<DeleteHalfYearlyReturnCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteHalfYearlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteHalfYearlyReturnCommand request, CancellationToken cancellationToken)
        {
            var HalfYearlyReturn = await _unitOfWork.Repository<HalfYearlyReturn>().Get(request.HalfYearlyReturnId);

            if (HalfYearlyReturn == null)
                throw new NotFoundException(nameof(HalfYearlyReturn), request.HalfYearlyReturnId);

            await _unitOfWork.Repository<HalfYearlyReturn>().Delete(HalfYearlyReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

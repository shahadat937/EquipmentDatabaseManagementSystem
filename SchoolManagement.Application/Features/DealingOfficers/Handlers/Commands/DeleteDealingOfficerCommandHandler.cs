using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DealingOfficers.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DealingOfficers.Handlers.Commands
{
    public class DeleteDealingOfficerCommandHandler : IRequestHandler<DeleteDealingOfficerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDealingOfficerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDealingOfficerCommand request, CancellationToken cancellationToken)
        {
            var DealingOfficer = await _unitOfWork.Repository<DealingOfficer>().Get(request.DealingOfficerId);

            if (DealingOfficer == null)
                throw new NotFoundException(nameof(DealingOfficer), request.DealingOfficerId);

            await _unitOfWork.Repository<DealingOfficer>().Delete(DealingOfficer);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

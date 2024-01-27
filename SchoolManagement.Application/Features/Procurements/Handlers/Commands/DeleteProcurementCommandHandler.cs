using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Procurements.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Commands
{
    public class DeleteProcurementCommandHandler : IRequestHandler<DeleteProcurementCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProcurementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProcurementCommand request, CancellationToken cancellationToken)
        {
            var Procurement = await _unitOfWork.Repository<Procurement>().Get(request.ProcurementId);

            if (Procurement == null)
                throw new NotFoundException(nameof(Procurement), request.ProcurementId);

            await _unitOfWork.Repository<Procurement>().Delete(Procurement);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

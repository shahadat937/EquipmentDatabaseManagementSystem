using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ProcurementTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProcurementTypes.Handlers.Commands
{
    public class DeleteProcurementTypeCommandHandler : IRequestHandler<DeleteProcurementTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProcurementTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProcurementTypeCommand request, CancellationToken cancellationToken)
        {
            var ProcurementType = await _unitOfWork.Repository<ProcurementType>().Get(request.ProcurementTypeId);

            if (ProcurementType == null)
                throw new NotFoundException(nameof(ProcurementType), request.ProcurementTypeId);

            await _unitOfWork.Repository<ProcurementType>().Delete(ProcurementType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ProcurementMethods.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProcurementMethods.Handlers.Commands
{
    public class DeleteProcurementMethodCommandHandler : IRequestHandler<DeleteProcurementMethodCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProcurementMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProcurementMethodCommand request, CancellationToken cancellationToken)
        {
            var ProcurementMethod = await _unitOfWork.Repository<ProcurementMethod>().Get(request.ProcurementMethodId);

            if (ProcurementMethod == null)
                throw new NotFoundException(nameof(ProcurementMethod), request.ProcurementMethodId);

            await _unitOfWork.Repository<ProcurementMethod>().Delete(ProcurementMethod);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

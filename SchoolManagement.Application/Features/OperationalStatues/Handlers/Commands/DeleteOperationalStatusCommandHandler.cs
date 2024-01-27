using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.OperationalStatuss.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.OperationalStatuss.Handlers.Commands
{
    public class DeleteOperationalStatusCommandHandler : IRequestHandler<DeleteOperationalStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOperationalStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOperationalStatusCommand request, CancellationToken cancellationToken)
        {
            var OperationalStatus = await _unitOfWork.Repository<OperationalStatus>().Get(request.OperationalStatusId);

            if (OperationalStatus == null)
                throw new NotFoundException(nameof(OperationalStatus), request.OperationalStatusId);

            await _unitOfWork.Repository<OperationalStatus>().Delete(OperationalStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

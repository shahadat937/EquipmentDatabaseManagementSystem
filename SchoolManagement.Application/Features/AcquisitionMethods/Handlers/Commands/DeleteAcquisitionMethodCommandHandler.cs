using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Handlers.Commands
{
    public class DeleteAcquisitionMethodCommandHandler : IRequestHandler<DeleteAcquisitionMethodCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteAcquisitionMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteAcquisitionMethodCommand request, CancellationToken cancellationToken)
        {
            var AcquisitionMethod = await _unitOfWork.Repository<AcquisitionMethod>().Get(request.AcquisitionMethodId);

            if (AcquisitionMethod == null)
                throw new NotFoundException(nameof(AcquisitionMethod), request.AcquisitionMethodId);

            await _unitOfWork.Repository<AcquisitionMethod>().Delete(AcquisitionMethod);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

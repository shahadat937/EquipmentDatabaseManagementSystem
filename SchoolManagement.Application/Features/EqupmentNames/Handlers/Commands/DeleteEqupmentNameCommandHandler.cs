using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EqupmentNames.Handlers.Commands
{
    public class DeleteEqupmentNameCommandHandler : IRequestHandler<DeleteEqupmentNameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEqupmentNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEqupmentNameCommand request, CancellationToken cancellationToken)
        {
            var EqupmentName = await _unitOfWork.Repository<EqupmentName>().Get(request.EqupmentNameId);

            if (EqupmentName == null)
                throw new NotFoundException(nameof(EqupmentName), request.EqupmentNameId);

            await _unitOfWork.Repository<EqupmentName>().Delete(EqupmentName);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

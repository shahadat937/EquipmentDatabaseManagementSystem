using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.EquipmentTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EquipmentTypes.Handlers.Commands
{
    public class DeleteEquipmentTypeCommandHandler : IRequestHandler<DeleteEquipmentTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEquipmentTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEquipmentTypeCommand request, CancellationToken cancellationToken)
        {
            var EquipmentType = await _unitOfWork.Repository<EquipmentType>().Get(request.EquipmentTypeId);

            if (EquipmentType == null)
                throw new NotFoundException(nameof(EquipmentType), request.EquipmentTypeId);

            await _unitOfWork.Repository<EquipmentType>().Delete(EquipmentType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

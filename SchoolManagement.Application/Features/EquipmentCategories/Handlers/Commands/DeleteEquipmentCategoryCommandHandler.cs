using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.EquipmentCategorys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Handlers.Commands
{
    public class DeleteEquipmentCategoryCommandHandler : IRequestHandler<DeleteEquipmentCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEquipmentCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEquipmentCategoryCommand request, CancellationToken cancellationToken)
        {
            var EquipmentCategory = await _unitOfWork.Repository<EquipmentCategory>().Get(request.EquipmentCategoryId);

            if (EquipmentCategory == null)
                throw new NotFoundException(nameof(EquipmentCategory), request.EquipmentCategoryId);

            await _unitOfWork.Repository<EquipmentCategory>().Delete(EquipmentCategory);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

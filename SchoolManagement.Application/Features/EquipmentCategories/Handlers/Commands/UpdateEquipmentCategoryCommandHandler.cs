using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EquipmentCategorys.Validators;
using SchoolManagement.Application.Features.EquipmentCategorys.Requests.Commands;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Handlers.Commands
{
    public class UpdateEquipmentCategoryCommandHandler : IRequestHandler<UpdateEquipmentCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEquipmentCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEquipmentCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEquipmentCategoryDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.EquipmentCategoryDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var EquipmentCategory = await _unitOfWork.Repository<EquipmentCategory>().Get(request.EquipmentCategoryDto.EquipmentCategoryId);

            if (EquipmentCategory is null)
                throw new NotFoundException(nameof(EquipmentCategory), request.EquipmentCategoryDto.EquipmentCategoryId);

            _mapper.Map(request.EquipmentCategoryDto, EquipmentCategory);

            await _unitOfWork.Repository<EquipmentCategory>().Update(EquipmentCategory);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

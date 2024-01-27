using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EquipmentCategorys.Validators;
using SchoolManagement.Application.Features.EquipmentCategorys.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EquipmentCategorys.Handlers.Commands
{
    public class CreateEquipmentCategoryCommandHandler : IRequestHandler<CreateEquipmentCategoryCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEquipmentCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateEquipmentCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEquipmentCategoryDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EquipmentCategoryDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var EquipmentCategory = _mapper.Map<EquipmentCategory>(request.EquipmentCategoryDto);

                EquipmentCategory = await _unitOfWork.Repository<EquipmentCategory>().Add(EquipmentCategory);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = EquipmentCategory.EquipmentCategoryId;
            }

            return response;
        }
    }
}

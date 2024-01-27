using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EquipmentType.Validators;
using SchoolManagement.Application.Features.EquipmentTypes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EquipmentTypes.Handlers.Commands
{
    public class CreateEquipmentTypeCommandHandler : IRequestHandler<CreateEquipmentTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEquipmentTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateEquipmentTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEquipmentTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EquipmentTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var EquipmentType = _mapper.Map<EquipmentType>(request.EquipmentTypeDto);

                EquipmentType = await _unitOfWork.Repository<EquipmentType>().Add(EquipmentType);

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
                response.Id = EquipmentType.EquipmentTypeId;
            }

            return response;
        }
    }
}

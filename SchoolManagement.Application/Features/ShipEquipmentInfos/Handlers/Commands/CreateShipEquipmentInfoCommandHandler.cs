using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo.Validators;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Commands
{
    public class CreateShipEquipmentInfoCommandHandler : IRequestHandler<CreateShipEquipmentInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShipEquipmentInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateShipEquipmentInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateShipEquipmentInfoDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ShipEquipmentInfoDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ShipEquipmentInfo = _mapper.Map<ShipEquipmentInfo>(request.ShipEquipmentInfoDto);

                ShipEquipmentInfo = await _unitOfWork.Repository<ShipEquipmentInfo>().Add(ShipEquipmentInfo);
                await _unitOfWork.Save();
                //try
                //{
                //    await _unitOfWork.Save();
                //}
                //catch (Exception ex)
                //{
                //    System.Console.WriteLine(ex);
                //}


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ShipEquipmentInfo.ShipEquipmentInfoId;
            }

            return response;
        }
    }
}

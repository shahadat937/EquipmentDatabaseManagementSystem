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

                string uniqueFileName = null;


                if (request.ShipEquipmentInfoDto.Doc != null)
                {
                    var fileName = Path.GetFileName(request.ShipEquipmentInfoDto.Doc.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\ship-equipment-files");

                    // Ensure the directory exists
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.ShipEquipmentInfoDto.Doc.CopyToAsync(fileStream);
                    }
                }

              
               
                var ShipEquipmentInfo = _mapper.Map<ShipEquipmentInfo>(request.ShipEquipmentInfoDto);

                if(uniqueFileName != null)
                {
                    ShipEquipmentInfo.FileUpload = request.ShipEquipmentInfoDto.FileUpload ?? "files/ship-equipment-files/" + uniqueFileName;

                }


                ShipEquipmentInfo = await _unitOfWork.Repository<ShipEquipmentInfo>().Add(ShipEquipmentInfo);
                await _unitOfWork.Save();
          

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ShipEquipmentInfo.ShipEquipmentInfoId;
            }

            return response;
        }
    }
}

using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipEquipmentInfo.Validators;
using SchoolManagement.Application.Features.ShipEquipmentInfos.Requests.Commands;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Features.ShipEquipmentInfos.Handlers.Commands
{
    public class UpdateShipEquipmentInfoCommandHandler : IRequestHandler<UpdateShipEquipmentInfoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UpdateShipEquipmentInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<Unit> Handle(UpdateShipEquipmentInfoCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateShipEquipmentInfoDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.ShipEquipmentInfoDto);

            var apiUrl = _config["ApiUrl"];

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);



            var ShipEquipmentInfo = await _unitOfWork.Repository<ShipEquipmentInfo>().Get(request.ShipEquipmentInfoDto.ShipEquipmentInfoId);

            if (ShipEquipmentInfo is null)
                throw new NotFoundException(nameof(ShipEquipmentInfo), request.ShipEquipmentInfoDto.ShipEquipmentInfoId);

            string uniqueFileName = "";

            if (request.ShipEquipmentInfoDto.Doc != null)
            {
                var fileName = Path.GetFileName(request.ShipEquipmentInfoDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\ship-equipment-files", uniqueFileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.ShipEquipmentInfoDto.Doc.CopyToAsync(fileSteam);
                }
            }

            _mapper.Map(request.ShipEquipmentInfoDto, ShipEquipmentInfo);

            if ((request.ShipEquipmentInfoDto.FileUpload != null && request.ShipEquipmentInfoDto.Doc != null) || (request.ShipEquipmentInfoDto.Doc == null && request.ShipEquipmentInfoDto.FileUpload != null)  || (request.ShipEquipmentInfoDto.Doc != null && request.ShipEquipmentInfoDto.FileUpload == null))
            {
                ShipEquipmentInfo.FileUpload = request.ShipEquipmentInfoDto.Doc != null ? "files/ship-equipment-files/" + uniqueFileName : ShipEquipmentInfo.FileUpload.Replace(apiUrl, String.Empty);


            }
            else if (request.ShipEquipmentInfoDto.FileUpload != null)
            {
                ShipEquipmentInfo.FileUpload = ShipEquipmentInfo.FileUpload.Replace(apiUrl, string.Empty);
            }
            else
            {
                ShipEquipmentInfo.FileUpload = "";
            }

            await _unitOfWork.Repository<ShipEquipmentInfo>().Update(ShipEquipmentInfo);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

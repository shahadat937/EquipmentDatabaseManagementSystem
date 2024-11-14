using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipInformations.Validators;
using SchoolManagement.Application.Features.ShipInformations.Requests.Commands;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Commands
{
    public class UpdateShipInformationCommandHandler : IRequestHandler<UpdateShipInformationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UpdateShipInformationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<Unit> Handle(UpdateShipInformationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateShipInformationDtoValidator();
            var apiUrl = _config["ApiUrl"];
            var validationResult = await validator.ValidateAsync(request.ShipInformationDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ShipInformation = await _unitOfWork.Repository<ShipInformation>().Get(request.ShipInformationDto.ShipInformationId);

            if (ShipInformation is null)
                throw new NotFoundException(nameof(ShipInformation), request.ShipInformationDto.ShipInformationId);

            string uniqueFileName = "";

            if (request.ShipInformationDto.Doc != null)
            {
                var fileName = Path.GetFileName(request.ShipInformationDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\images\\ship-information", uniqueFileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.ShipInformationDto.Doc.CopyToAsync(fileSteam);
                }
            }

            _mapper.Map(request.ShipInformationDto, ShipInformation);


            if ((request.ShipInformationDto.FileUpload != null && request.ShipInformationDto.Doc != null) || request.ShipInformationDto.Doc == null && request.ShipInformationDto.FileUpload != null)
            {
                ShipInformation.FileUpload = request.ShipInformationDto.Doc != null ? "images/ship-information/" + uniqueFileName : ShipInformation.FileUpload.Replace(apiUrl, String.Empty);


            }
            else if (request.ShipInformationDto.FileUpload != null)
            {
                ShipInformation.FileUpload = ShipInformation.FileUpload.Replace(apiUrl, string.Empty);
            }
            else
            {
                ShipInformation.FileUpload = "";
            }

            await _unitOfWork.Repository<ShipInformation>().Update(ShipInformation);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipDrowings.Validators;
using SchoolManagement.Application.Features.ShipDrowings.Requests.Commands;
using Microsoft.Extensions.Configuration;


namespace SchoolManagement.Application.Features.ShipDrowings.Handlers.Commands
{
    public class UpdateShipDrowingCommandHandler : IRequestHandler<UpdateShipDrowingCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UpdateShipDrowingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<Unit> Handle(UpdateShipDrowingCommand request, CancellationToken cancellationToken)
        {
            var apiUrl = _config["ApiUrl"];
            var validator = new UpdateShipDrowingDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ShipDrowingDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ShipDrowing = await _unitOfWork.Repository<ShipDrowing>().Get(request.ShipDrowingDto.ShipDrowingId);

            if (ShipDrowing is null)
                throw new NotFoundException(nameof(ShipDrowing), request.ShipDrowingDto.ShipDrowingId);

            string uniqueFileName = null;

            if (request.ShipDrowingDto.Doc != null)
            {

                var fileName = Path.GetFileName(request.ShipDrowingDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\ship-drowing", uniqueFileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.ShipDrowingDto.Doc.CopyToAsync(fileSteam);
                }
            }

            _mapper.Map(request.ShipDrowingDto, ShipDrowing);


            if ((request.ShipDrowingDto.FileUpload != null && request.ShipDrowingDto.Doc != null) || request.ShipDrowingDto.Doc == null && request.ShipDrowingDto.FileUpload != null)
            {
                ShipDrowing.FileUpload = request.ShipDrowingDto.Doc != null ? "files/ship-drowing/" + uniqueFileName : ShipDrowing.FileUpload.Replace(apiUrl, String.Empty);


            }
            else if (request.ShipDrowingDto.FileUpload != null)
            {
                ShipDrowing.FileUpload = ShipDrowing.FileUpload.Replace(apiUrl, string.Empty);
            }
            else
            {
                ShipDrowing.FileUpload = "";
            }

            // var Procurement = _mapper.Map<Procurement>(request.ProcurementDto);

            //ShipDrowing = await _unitOfWork.Repository<ShipDrowing>().Add(ShipDrowing);

          
            try
            {
                await _unitOfWork.Repository<ShipDrowing>().Update(ShipDrowing);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Unit.Value;
        }
    }
}

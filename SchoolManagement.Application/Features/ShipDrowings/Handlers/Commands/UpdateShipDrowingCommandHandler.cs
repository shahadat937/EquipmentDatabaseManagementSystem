using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipDrowings.Validators;
using SchoolManagement.Application.Features.ShipDrowings.Requests.Commands;

namespace SchoolManagement.Application.Features.ShipDrowings.Handlers.Commands
{
    public class UpdateShipDrowingCommandHandler : IRequestHandler<UpdateShipDrowingCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShipDrowingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateShipDrowingCommand request, CancellationToken cancellationToken)
        {
            //var validator = new UpdateShipDrowingDtoValidator(); 
            // var validationResult = await validator.ValidateAsync(request.ShipDrowingDto);

            //if (validationResult.IsValid == false)
            //    throw new ValidationException(validationResult);

            //var ShipDrowing = await _unitOfWork.Repository<ShipDrowing>().Get(request.ShipDrowingDto.ShipDrowingId);

            //if (ShipDrowing is null)
            //    throw new NotFoundException(nameof(ShipDrowing), request.ShipDrowingDto.ShipDrowingId);

            //_mapper.Map(request.ShipDrowingDto, ShipDrowing);

            /////// File Upload //////////
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

            var ShipDrowing = _mapper.Map<ShipDrowing>(request.ShipDrowingDto);

            //  ShipDrowing.Date = ShipDrowing.Date.Value.AddDays(1.0);

            // var Procurement = _mapper.Map<Procurement>(request.ProcurementDto);
            ShipDrowing.FileUpload = request.ShipDrowingDto.FileUpload ?? "files/ship-drowing/" + uniqueFileName;

            //ShipDrowing = await _unitOfWork.Repository<ShipDrowing>().Add(ShipDrowing);

            await _unitOfWork.Repository<ShipDrowing>().Update(ShipDrowing);
            try
            {
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

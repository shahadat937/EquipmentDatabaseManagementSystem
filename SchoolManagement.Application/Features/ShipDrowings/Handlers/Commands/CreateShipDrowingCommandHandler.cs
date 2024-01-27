using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipDrowings.Validators;
using SchoolManagement.Application.Features.ShipDrowings.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipDrowings.Handlers.Commands
{
    public class CreateShipDrowingCommandHandler : IRequestHandler<CreateShipDrowingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShipDrowingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateShipDrowingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateShipDrowingDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ShipDrowingDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
              

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

            ShipDrowing = await _unitOfWork.Repository<ShipDrowing>().Add(ShipDrowing);

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
                response.Id = ShipDrowing.ShipDrowingId;
            }

            return response;
        }
    }
}

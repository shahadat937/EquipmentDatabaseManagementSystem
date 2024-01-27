using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShipInformations.Validators;
using SchoolManagement.Application.Features.ShipInformations.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Commands
{
    public class CreateShipInformationCommandHandler : IRequestHandler<CreateShipInformationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateShipInformationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateShipInformationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateShipInformationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ShipInformationDto);

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

                var ShipInformation = _mapper.Map<ShipInformation>(request.ShipInformationDto);
                ShipInformation.FileUpload = request.ShipInformationDto.FileUpload ?? "images/ship-information/" + uniqueFileName;

                ShipInformation = await _unitOfWork.Repository<ShipInformation>().Add(ShipInformation);

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
                response.Id = ShipInformation.ShipInformationId;
            }

            return response;
        }
    }
}

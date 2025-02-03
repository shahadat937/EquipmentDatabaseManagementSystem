using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands;
using Microsoft.Extensions.Configuration;


namespace SchoolManagement.Application.Features.MonthlyReturns.Handlers.Commands
{
    public class UpdateMonthlyReturnCommandHandler : IRequestHandler<UpdateMonthlyReturnCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _shipEquipmentRepository;
        private readonly IConfiguration _config;

        public UpdateMonthlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISchoolManagementRepository<ShipEquipmentInfo> shipEquipmentRepository, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _shipEquipmentRepository = shipEquipmentRepository;
            _config = config;
        }

        public async Task<Unit> Handle(UpdateMonthlyReturnCommand request, CancellationToken cancellationToken)
        {
            var apiUrl = _config["ApiUrl"];
            var validator = new UpdateMonthlyReturnDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.MonthlyReturnDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var MonthlyReturn = await _unitOfWork.Repository<MonthlyReturn>().Get(request.MonthlyReturnDto.MonthlyReturnId);

            if (MonthlyReturn is null)
                throw new NotFoundException(nameof(MonthlyReturn), request.MonthlyReturnDto.MonthlyReturnId);

            var ShipEquipmentInfo = await _shipEquipmentRepository.Get(request.MonthlyReturnDto.ShipEquipmentInfoId ?? 0);
            bool isQtyChange = false;

            var existingReturnQty = MonthlyReturn.ReturnQty;

            if(existingReturnQty < request.MonthlyReturnDto.ReturnQty)
            {
                var difference = existingReturnQty - request.MonthlyReturnDto.ReturnQty;
                ShipEquipmentInfo.OplQty += difference;
                ShipEquipmentInfo.NonOplQty -= difference;
                isQtyChange = true;
            }
            else if (existingReturnQty > request.MonthlyReturnDto.ReturnQty)
            {
                var difference =  request.MonthlyReturnDto.ReturnQty - existingReturnQty ;
                ShipEquipmentInfo.OplQty -= difference;
                ShipEquipmentInfo.NonOplQty += difference;
                isQtyChange = true;
            }

            var uniqueFileName = "";

            if (request.MonthlyReturnDto.Doc != null)
            {
                var fileName = Path.GetFileName(request.MonthlyReturnDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\damage-electrical", uniqueFileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.MonthlyReturnDto.Doc.CopyToAsync(fileSteam);
                }
            }


            _mapper.Map(request.MonthlyReturnDto, MonthlyReturn);


            if ((request.MonthlyReturnDto.UploadDocument != null && request.MonthlyReturnDto.Doc != null) || request.MonthlyReturnDto.Doc == null && request.MonthlyReturnDto.UploadDocument != null)
            {
                MonthlyReturn.UploadDocument = request.MonthlyReturnDto.Doc != null ? "files/damage-electrical/" + uniqueFileName : MonthlyReturn.UploadDocument.Replace(apiUrl, String.Empty);


            }
            else if (request.MonthlyReturnDto.UploadDocument != null)
            {
                MonthlyReturn.UploadDocument = MonthlyReturn.UploadDocument.Replace(apiUrl, string.Empty);
            }
            else
            {
                MonthlyReturn.UploadDocument = "";
            }

            await _unitOfWork.Repository<MonthlyReturn>().Update(MonthlyReturn);
            await _unitOfWork.Save();

            if (isQtyChange)
            {
                await _unitOfWork.Repository<ShipEquipmentInfo>().Update(ShipEquipmentInfo);
                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}

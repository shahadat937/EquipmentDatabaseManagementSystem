using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System.Diagnostics.Metrics;
using System;

namespace SchoolManagement.Application.Features.MonthlyReturns.Handlers.Commands
{
    public class CreateMonthlyReturnCommandHandler : IRequestHandler<CreateMonthlyReturnCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShipEquipmentInfo> _shipEquipmentRepository;

        public CreateMonthlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISchoolManagementRepository<ShipEquipmentInfo> shipEquipmentRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _shipEquipmentRepository = shipEquipmentRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateMonthlyReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateMonthlyReturnDtoValidator();
            var validationResult = await validator.ValidateAsync(request.MonthlyReturnDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {

                var shipEquipmentData = await _shipEquipmentRepository.Get(request.MonthlyReturnDto.ShipEquipmentInfoId ?? 0);

                shipEquipmentData.OplQty -=  request.MonthlyReturnDto.ReturnQty;
                shipEquipmentData.NonOplQty += request.MonthlyReturnDto.ReturnQty;
                shipEquipmentData.LastRetrunModificationDate = DateTime.Now;

                string uniqueFileName = null;


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

                var MonthlyReturn = _mapper.Map<MonthlyReturn>(request.MonthlyReturnDto);

                if(uniqueFileName == null)
                {
                    MonthlyReturn.UploadDocument = request.MonthlyReturnDto.UploadDocument ?? "files/damage-electrical/" + uniqueFileName;

                }

                MonthlyReturn = await _unitOfWork.Repository<MonthlyReturn>().Add(MonthlyReturn);

                try
                {
                    await _unitOfWork.Save();

                    await _unitOfWork.Repository<ShipEquipmentInfo>().Update(shipEquipmentData);
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                //var MonthlyReturn = _mapper.Map<MonthlyReturn>(request.MonthlyReturnDto);

                //MonthlyReturn = await _unitOfWork.Repository<MonthlyReturn>().Add(MonthlyReturn);

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
                response.Id = MonthlyReturn.MonthlyReturnId;
            }

            return response;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.HalfYearlyReturn.Validators;
using SchoolManagement.Application.Features.HalfYearlyReturns.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.HalfYearlyReturns.Handlers.Commands
{
    public class CreateHalfYearlyReturnCommandHandler : IRequestHandler<CreateHalfYearlyReturnCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHalfYearlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateHalfYearlyReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Content", "files", "half-yearly-returns");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var helfyearlyInfo in request.HalfYearlyReturnDto.ShipEquipmentInfoList)
                {
                    string uniqueFileName = null;

                    if (helfyearlyInfo.Doc != null)
                    {
                        var fileName = Path.GetFileName(helfyearlyInfo.Doc.FileName);
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);


                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await helfyearlyInfo.Doc.CopyToAsync(fileStream);
                        }
                    }
                    var halfYearly = new HalfYearlyReturn
                    {
                        BaseSchoolNameId = helfyearlyInfo.BaseSchoolNameId,
                        EquipmentCategoryId = request.HalfYearlyReturnDto.EquipmentCategoryId,
                        EqupmentNameId = request.HalfYearlyReturnDto.EqupmentNameId,
                        BrandId = request.HalfYearlyReturnDto.BrandId,
                        HalfYearlyRunningTime = helfyearlyInfo.HalfYearlyRunningTime,
                        TotalRunningTime = helfyearlyInfo.TotalRunningTime,
                        HalfYearlyProblem = helfyearlyInfo.HalfYearlyProblem,
                        PowerSupplyUnit = helfyearlyInfo.PowerSupplyUnit,
                        Remarks = helfyearlyInfo.Remarks,
                        IsSatisfactory = helfyearlyInfo.IsSatisfactory,
                        ShipEquipmentInfoId = helfyearlyInfo.ShipEquipmentInfoId,
                        ReportingMonthId = request.HalfYearlyReturnDto.ReportingMonthId,
                        Year = request.HalfYearlyReturnDto.Year,
                        UploadDocument = uniqueFileName != null ? "files/half-yearly-returns/" + uniqueFileName : null
                    };

                    // Add the entity to the repository
                    await _unitOfWork.Repository<HalfYearlyReturn>().Add(halfYearly);
                }

                // Save all changes to the database
                await _unitOfWork.Save();

                // Set the response
                response.Success = true;
                response.Message = "Creation Successful";
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a proper logging framework here)
                Console.WriteLine(ex);

                // Set the response for failure
                response.Success = false;
                response.Message = "An error occurred while creating the records.";
                response.Errors = new List<string> { ex.Message }; // Add error details if needed
            }

            return response;
        }
    }
}

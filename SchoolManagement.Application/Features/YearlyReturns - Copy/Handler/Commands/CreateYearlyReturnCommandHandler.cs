using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.DTOs.YearlyReturns.Validators;
using SchoolManagement.Application.Features.YearlyReturns.Request.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Handler.Commands
{
    public class CreateYearlyReturnCommandHandler : IRequestHandler<CreateYearlyReturnCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateYearlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateYearlyReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

         
            var validator = new CreateYearlyReturnDtoValidator();
            var validationResult = await validator.ValidateAsync(request.YearlyReturnDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {

                string uniqueFileName = null;

                //if (request.YearlyReturnDto.Doc != null)
                //{

                //    var fileName = Path.GetFileName(request.YearlyReturnDto.Doc.FileName);
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                //    var a = Directory.GetCurrentDirectory();
                //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\ship-drowing", uniqueFileName);

                //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                //    {
                //        await request.YearlyReturnDto.Doc.CopyToAsync(fileSteam);
                //    }
                //}

                if (request.YearlyReturnDto.Doc != null)
                {
                    var fileName = Path.GetFileName(request.YearlyReturnDto.Doc.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;


                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\yearly-return");


                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var filePath = Path.Combine(folderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.YearlyReturnDto.Doc.CopyToAsync(fileStream);
                    }
                }



                var yearlyReturn = _mapper.Map<YearlyReturn>(request.YearlyReturnDto);
                yearlyReturn.FileUpload = request.YearlyReturnDto.FileUpload ?? "files/yearly-return/" + uniqueFileName;
                yearlyReturn = await _unitOfWork.Repository<YearlyReturn>().Add(yearlyReturn);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
           
                    response.Success = false;
                    response.Message = "Error occurred while saving.";
                    response.Errors = new List<string> { ex.Message };
                    return response;
                }

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = yearlyReturn.YearlyReturnId;
            }

            return response;
        }
    }

}

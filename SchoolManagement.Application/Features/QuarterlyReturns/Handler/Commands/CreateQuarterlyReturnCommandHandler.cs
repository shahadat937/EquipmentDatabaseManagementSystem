using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.DTOs.QuarterlyReturns.Validators;
using SchoolManagement.Application.Features.QuarterlyReturns.Request.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Handler.Commands
{
    public class CreateQuarterlyReturnCommandHandler : IRequestHandler<CreateQuarterlyReturnCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateQuarterlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateQuarterlyReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

         
            var validator = new CreateQuarterlyReturnDtoValidator();
            var validationResult = await validator.ValidateAsync(request.QuarterlyReturnDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {

                string uniqueFileName = null;

              

                if (request.QuarterlyReturnDto.Doc != null)
                {
                    var fileName = Path.GetFileName(request.QuarterlyReturnDto.Doc.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;


                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\Quarterly-return");


                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var filePath = Path.Combine(folderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.QuarterlyReturnDto.Doc.CopyToAsync(fileStream);
                    }
                }



                var QuarterlyReturn = _mapper.Map<QuarterlyReturn>(request.QuarterlyReturnDto);

                if(uniqueFileName != null)
                {
                    QuarterlyReturn.FileUpload = request.QuarterlyReturnDto.FileUpload ?? "files/Quarterly-return/" + uniqueFileName;
                }

                QuarterlyReturn = await _unitOfWork.Repository<QuarterlyReturn>().Add(QuarterlyReturn);

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
                response.Id = QuarterlyReturn.QuarterlyReturnId;
            }

            return response;
        }
    }

}

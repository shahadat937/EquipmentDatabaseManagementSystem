using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.DTOs.YearlyReturns.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands;
using SchoolManagement.Application.Features.YearlyReturns.Request.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = SchoolManagement.Application.Exceptions.ValidationException;

namespace SchoolManagement.Application.Features.YearlyReturns.Handler.Commands
{
    public class UpdateYearlyReturnCommandHandler : IRequestHandler<UpdateYearlyReturnCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UpdateYearlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<Unit> Handle(UpdateYearlyReturnCommand request, CancellationToken cancellationToken)
        {
            var apiUrl = _config["ApiUrl"];
            var validator = new UpdateYearlyReturnValidator();
            var validationResult = await validator.ValidateAsync(request.YearlyReturnDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var yearlyReturn = await _unitOfWork.Repository<YearlyReturn>().Get(request.YearlyReturnDto.YearlyReturnId);

            if (yearlyReturn is null)
                throw new NotFoundException(nameof(YearlyReturn), request.YearlyReturnDto.YearlyReturnId);

            string uniqueFileName = null;

            if (request.YearlyReturnDto.Doc != null)
            {

                var fileName = Path.GetFileName(request.YearlyReturnDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\yearly-return", uniqueFileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.YearlyReturnDto.Doc.CopyToAsync(fileSteam);
                }
            }

            _mapper.Map(request.YearlyReturnDto, yearlyReturn);

            if ((request.YearlyReturnDto.FileUpload != null && request.YearlyReturnDto.Doc != null) || request.YearlyReturnDto.Doc == null && request.YearlyReturnDto.FileUpload != null)
            {
                yearlyReturn.FileUpload = request.YearlyReturnDto.Doc != null ? "files/yearly-return/" + uniqueFileName : yearlyReturn.FileUpload.Replace(apiUrl, String.Empty);


            }
            else if (request.YearlyReturnDto.FileUpload != null)
            {
                yearlyReturn.FileUpload = yearlyReturn.FileUpload.Replace(apiUrl, string.Empty);
            }
            else
            {
                yearlyReturn.FileUpload = "";
            }


            await _unitOfWork.Repository<YearlyReturn>().Update(yearlyReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

}

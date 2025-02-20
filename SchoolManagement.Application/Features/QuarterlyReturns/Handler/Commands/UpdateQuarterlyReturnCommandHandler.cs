using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MonthlyReturns.Validators;
using SchoolManagement.Application.DTOs.QuarterlyReturns.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.MonthlyReturns.Requests.Commands;
using SchoolManagement.Application.Features.QuarterlyReturns.Request.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = SchoolManagement.Application.Exceptions.ValidationException;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Handler.Commands
{
    public class UpdateQuarterlyReturnCommandHandler : IRequestHandler<UpdateQuarterlyReturnCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UpdateQuarterlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }

        public async Task<Unit> Handle(UpdateQuarterlyReturnCommand request, CancellationToken cancellationToken)
        {
            var apiUrl = _config["ApiUrl"];
            var validator = new UpdateQuarterlyReturnValidator();
            var validationResult = await validator.ValidateAsync(request.QuarterlyReturnDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var QuarterlyReturn = await _unitOfWork.Repository<QuarterlyReturn>().Get(request.QuarterlyReturnDto.QuarterlyReturnId);

            if (QuarterlyReturn is null)
                throw new NotFoundException(nameof(QuarterlyReturn), request.QuarterlyReturnDto.QuarterlyReturnId);

            string uniqueFileName = null;

            if (request.QuarterlyReturnDto.Doc != null)
            {

                var fileName = Path.GetFileName(request.QuarterlyReturnDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\Quarterly-return", uniqueFileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.QuarterlyReturnDto.Doc.CopyToAsync(fileSteam);
                }
            }

            _mapper.Map(request.QuarterlyReturnDto, QuarterlyReturn);

            if (request.QuarterlyReturnDto.FileUpload != null)
            {
                QuarterlyReturn.FileUpload = request.QuarterlyReturnDto.Doc != null
                    ? "files/Quarterly-return/" + uniqueFileName
                    : QuarterlyReturn.FileUpload.Replace(apiUrl, string.Empty);
            }
            else
            {
                QuarterlyReturn.FileUpload = "";
            }


            await _unitOfWork.Repository<QuarterlyReturn>().Update(QuarterlyReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

}

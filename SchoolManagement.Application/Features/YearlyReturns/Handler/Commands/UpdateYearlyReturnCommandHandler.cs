using AutoMapper;
using FluentValidation;
using MediatR;
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

        public UpdateYearlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateYearlyReturnCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateYearlyReturnValidator();
            var validationResult = await validator.ValidateAsync(request.YearlyReturnDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var yearlyReturn = await _unitOfWork.Repository<YearlyReturn>().Get(request.YearlyReturnDto.YearlyReturnId);

            if (yearlyReturn is null)
                throw new NotFoundException(nameof(YearlyReturn), request.YearlyReturnDto.YearlyReturnId);

            _mapper.Map(request.YearlyReturnDto, yearlyReturn);

            await _unitOfWork.Repository<YearlyReturn>().Update(yearlyReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

}

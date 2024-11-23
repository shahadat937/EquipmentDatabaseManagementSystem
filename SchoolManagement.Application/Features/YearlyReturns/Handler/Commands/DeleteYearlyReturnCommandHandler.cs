using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.YearlyReturns.Request.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.YearlyReturns.Handler.Commands
{
    public class DeleteYearlyReturnCommandHandler : IRequestHandler<DeleteYearlyReturnCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteYearlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteYearlyReturnCommand request, CancellationToken cancellationToken)
        {
            // Fetch the YearlyReturn record by ID
            var yearlyReturn = await _unitOfWork.Repository<YearlyReturn>().Get(request.YearlyReturnId);

            // If not found, throw a NotFoundException
            if (yearlyReturn == null)
                throw new NotFoundException(nameof(YearlyReturn), request.YearlyReturnId);

            // Delete the record
            await _unitOfWork.Repository<YearlyReturn>().Delete(yearlyReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

}

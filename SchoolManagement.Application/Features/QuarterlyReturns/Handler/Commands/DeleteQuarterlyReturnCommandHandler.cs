using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.QuarterlyReturns.Request.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.QuarterlyReturns.Handler.Commands
{
    public class DeleteQuarterlyReturnCommandHandler : IRequestHandler<DeleteQuarterlyReturnCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteQuarterlyReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteQuarterlyReturnCommand request, CancellationToken cancellationToken)
        {
            // Fetch the QuarterlyReturn record by ID
            var QuarterlyReturn = await _unitOfWork.Repository<QuarterlyReturn>().Get(request.QuarterlyReturnId);

            // If not found, throw a NotFoundException
            if (QuarterlyReturn == null)
                throw new NotFoundException(nameof(QuarterlyReturn), request.QuarterlyReturnId);

            // Delete the record
            await _unitOfWork.Repository<QuarterlyReturn>().Delete(QuarterlyReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }

}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.StatusOfShip.Request.Commands;
using SchoolManagement.Application.Features.YearlyReturns.Request.Commands;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StatusOfShip.Handler.Command
{
    public class DeleteStatusOfShipCommandHandler: IRequestHandler<DeleteStatusOfShipCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteStatusOfShipCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteStatusOfShipCommand request, CancellationToken cancellationToken)
        {
            var statusOfShip = await _unitOfWork.Repository<Domain.StatusOfShip>().Get(request.StatusOfShipId);

            // If not found, throw a NotFoundException
            if (statusOfShip == null)
                throw new NotFoundException(nameof(Domain.StatusOfShip), request.StatusOfShipId);

            // Delete the record
            await _unitOfWork.Repository<Domain.StatusOfShip>().Delete(statusOfShip);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

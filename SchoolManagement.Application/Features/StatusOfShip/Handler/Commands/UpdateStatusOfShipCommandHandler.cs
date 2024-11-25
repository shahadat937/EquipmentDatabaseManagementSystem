using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.StatusOfShip.Validators;
using SchoolManagement.Application.DTOs.YearlyReturns.Validators;
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
    public class UpdateStatusOfShipCommandHandler : IRequestHandler<UpdateStatusOfShipCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStatusOfShipCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateStatusOfShipCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateStatusOfShipDtoValidator();
            var validationResult = await validator.ValidateAsync(request.StatusOfShipDto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var statusOfShip = await _unitOfWork.Repository<Domain.StatusOfShip>().Get(request.StatusOfShipDto.StatusOfShipId);

            if (statusOfShip is null)
                throw new NotFoundException(nameof(StatusOfShip), request.StatusOfShipDto.StatusOfShipId);

            _mapper.Map(request.StatusOfShipDto, statusOfShip);

            await _unitOfWork.Repository<Domain.StatusOfShip>().Update(statusOfShip);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.OperationalStatuss.Validators;
using SchoolManagement.Application.DTOs.OperationalStatusOfEquipmentSystem.Validator;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Handlers.Commands
{
    public class UpdateOperationalStatusOfEquipmentSystemCommandHandler : IRequestHandler<UpdateOperationalStatusOfEquipmentSystemCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOperationalStatusOfEquipmentSystemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOperationalStatusOfEquipmentSystemCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOperationalStatusOfEquipmentSystemDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OperationalStatusOfEquipmentSystemDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var OperationalStatus = await _unitOfWork.Repository<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem>().Get(request.OperationalStatusOfEquipmentSystemDto.OperationalStatusOfEquipmentSystemId);

            if (OperationalStatus is null)
                throw new NotFoundException(nameof(OperationalStatus), request.OperationalStatusOfEquipmentSystemDto.OperationalStatusOfEquipmentSystemId);

            _mapper.Map(request.OperationalStatusOfEquipmentSystemDto, OperationalStatus);

            await _unitOfWork.Repository<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem>().Update(OperationalStatus);
            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {

            }
           

            return Unit.Value;
        }
    }
}

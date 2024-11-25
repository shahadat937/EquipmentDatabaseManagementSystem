using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.OperationalStatusOfEquipmentSystem.Handlers.Commands
{
    public class DeleteOperationalStatusOfEquipmentSystemCommandHandler : IRequestHandler<DeleteOperationalStatusOfEquipmentSystemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOperationalStatusOfEquipmentSystemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteOperationalStatusOfEquipmentSystemCommand request, CancellationToken cancellationToken)
        {

            var opStatus = await _unitOfWork.Repository<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem>().Get(request.OperationalStatusOfEquipmentSystemId);

            if (opStatus == null)
                throw new NotFoundException(nameof(opStatus), request.OperationalStatusOfEquipmentSystemId);

            await _unitOfWork.Repository<SchoolManagement.Domain.OperationalStatusOfEquipmentSystem>().Delete(opStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

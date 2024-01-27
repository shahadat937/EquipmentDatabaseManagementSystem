using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.StateOfEquipments.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.StateOfEquipments.Handlers.Commands
{
    public class DeleteStateOfEquipmentCommandHandler : IRequestHandler<DeleteStateOfEquipmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteStateOfEquipmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteStateOfEquipmentCommand request, CancellationToken cancellationToken)
        {
            var StateOfEquipment = await _unitOfWork.Repository<StateOfEquipment>().Get(request.StateOfEquipmentId);

            if (StateOfEquipment == null)
                throw new NotFoundException(nameof(StateOfEquipment), request.StateOfEquipmentId);

            await _unitOfWork.Repository<StateOfEquipment>().Delete(StateOfEquipment);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

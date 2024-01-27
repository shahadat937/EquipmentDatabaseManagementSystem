using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShipInformations.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShipInformations.Handlers.Commands
{
    public class DeleteShipInformationCommandHandler : IRequestHandler<DeleteShipInformationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteShipInformationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteShipInformationCommand request, CancellationToken cancellationToken)
        {
            var ShipInformation = await _unitOfWork.Repository<ShipInformation>().Get(request.ShipInformationId);

            if (ShipInformation == null)
                throw new NotFoundException(nameof(ShipInformation), request.ShipInformationId);

            await _unitOfWork.Repository<ShipInformation>().Delete(ShipInformation);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

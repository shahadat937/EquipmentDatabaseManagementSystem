using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DgdpNssds.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DgdpNssds.Handlers.Commands
{
    public class DeleteDgdpNssdCommandHandler : IRequestHandler<DeleteDgdpNssdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDgdpNssdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDgdpNssdCommand request, CancellationToken cancellationToken)
        {
            var DgdpNssd = await _unitOfWork.Repository<DgdpNssd>().Get(request.DgdpNssdId);

            if (DgdpNssd == null)
                throw new NotFoundException(nameof(DgdpNssd), request.DgdpNssdId);

            await _unitOfWork.Repository<DgdpNssd>().Delete(DgdpNssd);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

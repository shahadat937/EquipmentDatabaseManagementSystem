using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Tecs.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Tecs.Handlers.Commands
{
    public class DeleteTecCommandHandler : IRequestHandler<DeleteTecCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTecCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteTecCommand request, CancellationToken cancellationToken)
        {
            var Tec = await _unitOfWork.Repository<Tec>().Get(request.TecId);

            if (Tec == null)
                throw new NotFoundException(nameof(Tec), request.TecId);

            await _unitOfWork.Repository<Tec>().Delete(Tec);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

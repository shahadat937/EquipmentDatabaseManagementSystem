using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Sqns.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Sqns.Handlers.Commands
{
    public class DeleteSqnCommandHandler : IRequestHandler<DeleteSqnCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSqnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSqnCommand request, CancellationToken cancellationToken)
        {
            var Sqn = await _unitOfWork.Repository<Sqn>().Get(request.SqnId);

            if (Sqn == null)
                throw new NotFoundException(nameof(Sqn), request.SqnId);

            await _unitOfWork.Repository<Sqn>().Delete(Sqn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

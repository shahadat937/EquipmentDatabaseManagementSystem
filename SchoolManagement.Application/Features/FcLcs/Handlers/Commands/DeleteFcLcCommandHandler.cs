using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FcLcs.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FcLcs.Handlers.Commands
{
    public class DeleteFcLcCommandHandler : IRequestHandler<DeleteFcLcCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFcLcCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFcLcCommand request, CancellationToken cancellationToken)
        {
            var FcLc = await _unitOfWork.Repository<FcLc>().Get(request.FcLcId);

            if (FcLc == null)
                throw new NotFoundException(nameof(FcLc), request.FcLcId);

            await _unitOfWork.Repository<FcLc>().Delete(FcLc);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Handlers.Commands
{
    public class DeleteQuarterlyReturnNoTypeCommandHandler : IRequestHandler<DeleteQuarterlyReturnNoTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteQuarterlyReturnNoTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteQuarterlyReturnNoTypeCommand request, CancellationToken cancellationToken)
        {
            var QuarterlyReturnNoType = await _unitOfWork.Repository<QuarterlyReturnNoType>().Get(request.QuarterlyReturnNoTypeId);

            if (QuarterlyReturnNoType == null)
                throw new NotFoundException(nameof(QuarterlyReturnNoType), request.QuarterlyReturnNoTypeId);

            await _unitOfWork.Repository<QuarterlyReturnNoType>().Delete(QuarterlyReturnNoType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

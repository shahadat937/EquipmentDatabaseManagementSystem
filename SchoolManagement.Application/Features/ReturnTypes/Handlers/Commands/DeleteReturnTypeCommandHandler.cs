using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ReturnTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReturnTypes.Handlers.Commands
{
    public class DeleteReturnTypeCommandHandler : IRequestHandler<DeleteReturnTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReturnTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteReturnTypeCommand request, CancellationToken cancellationToken)
        {
            var ReturnType = await _unitOfWork.Repository<ReturnType>().Get(request.ReturnTypeId);

            if (ReturnType == null)
                throw new NotFoundException(nameof(ReturnType), request.ReturnTypeId);

            await _unitOfWork.Repository<ReturnType>().Delete(ReturnType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

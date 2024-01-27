using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.LetterTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.LetterTypes.Handlers.Commands
{
    public class DeleteLetterTypeCommandHandler : IRequestHandler<DeleteLetterTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLetterTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLetterTypeCommand request, CancellationToken cancellationToken)
        {
            var LetterType = await _unitOfWork.Repository<LetterType>().Get(request.LetterTypeId);

            if (LetterType == null)
                throw new NotFoundException(nameof(LetterType), request.LetterTypeId);

            await _unitOfWork.Repository<LetterType>().Delete(LetterType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

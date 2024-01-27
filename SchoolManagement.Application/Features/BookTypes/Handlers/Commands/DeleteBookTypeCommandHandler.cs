using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.BookTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.BookTypes.Handlers.Commands
{
    public class DeleteBookTypeCommandHandler : IRequestHandler<DeleteBookTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBookTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBookTypeCommand request, CancellationToken cancellationToken)
        {
            var BookType = await _unitOfWork.Repository<BookType>().Get(request.BookTypeId);

            if (BookType == null)
                throw new NotFoundException(nameof(BookType), request.BookTypeId);

            await _unitOfWork.Repository<BookType>().Delete(BookType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

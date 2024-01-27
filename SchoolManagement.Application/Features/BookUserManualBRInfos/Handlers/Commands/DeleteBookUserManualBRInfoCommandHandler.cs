using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.BookUserManualBRInfos.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.BookUserManualBRInfos.Handlers.Commands
{
    public class DeleteBookUserManualBRInfoCommandHandler : IRequestHandler<DeleteBookUserManualBRInfoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBookUserManualBRInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBookUserManualBRInfoCommand request, CancellationToken cancellationToken)
        {
            var BookUserManualBRInfo = await _unitOfWork.Repository<BookUserManualBRInfo>().Get(request.BookUserManualBRInfoId);

            if (BookUserManualBRInfo == null)
                throw new NotFoundException(nameof(BookUserManualBRInfo), request.BookUserManualBRInfoId);

            await _unitOfWork.Repository<BookUserManualBRInfo>().Delete(BookUserManualBRInfo);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

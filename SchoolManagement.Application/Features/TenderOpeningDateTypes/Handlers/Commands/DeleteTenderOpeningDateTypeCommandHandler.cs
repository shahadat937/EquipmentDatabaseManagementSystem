using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.TenderOpeningDateTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.TenderOpeningDateTypes.Handlers.Commands
{
    public class DeleteTenderOpeningDateTypeCommandHandler : IRequestHandler<DeleteTenderOpeningDateTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTenderOpeningDateTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteTenderOpeningDateTypeCommand request, CancellationToken cancellationToken)
        {
            var TenderOpeningDateType = await _unitOfWork.Repository<TenderOpeningDateType>().Get(request.TenderOpeningDateTypeId);

            if (TenderOpeningDateType == null)
                throw new NotFoundException(nameof(TenderOpeningDateType), request.TenderOpeningDateTypeId);

            await _unitOfWork.Repository<TenderOpeningDateType>().Delete(TenderOpeningDateType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

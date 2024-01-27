using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ReportTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReportTypes.Handlers.Commands
{
    public class DeleteReportTypeCommandHandler : IRequestHandler<DeleteReportTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReportTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteReportTypeCommand request, CancellationToken cancellationToken)
        {
            var ReportType = await _unitOfWork.Repository<ReportType>().Get(request.ReportTypeId);

            if (ReportType == null)
                throw new NotFoundException(nameof(ReportType), request.ReportTypeId);

            await _unitOfWork.Repository<ReportType>().Delete(ReportType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

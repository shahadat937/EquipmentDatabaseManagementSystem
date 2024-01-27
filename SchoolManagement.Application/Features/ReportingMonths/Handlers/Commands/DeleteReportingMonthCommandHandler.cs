using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ReportingMonths.Requests.Commands;
using SchoolManagement.Doamin;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReportingMonths.Handlers.Commands
{
    public class DeleteReportingMonthCommandHandler : IRequestHandler<DeleteReportingMonthCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReportingMonthCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
         
        public async Task<Unit> Handle(DeleteReportingMonthCommand request, CancellationToken cancellationToken)
        {
            var ReportingMonth = await _unitOfWork.Repository<ReportingMonth>().Get(request.ReportingMonthId);

            if (ReportingMonth == null)
                throw new NotFoundException(nameof(ReportingMonth), request.ReportingMonthId);

            await _unitOfWork.Repository<ReportingMonth>().Delete(ReportingMonth);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

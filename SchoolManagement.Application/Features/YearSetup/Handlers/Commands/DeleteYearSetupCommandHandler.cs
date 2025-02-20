using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Commands
{
    public class DeleteYearSetupCommandHandler : IRequestHandler<DeleteYearSetupCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteYearSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteYearSetupCommand request, CancellationToken cancellationToken)
        {
            var YearSetup = await _unitOfWork.Repository<YearSetup>().Get(request.YearSetupId);

            if (YearSetup == null)
                throw new NotFoundException(nameof(YearSetup), request.YearSetupId);

            await _unitOfWork.Repository<YearSetup>().Delete(YearSetup);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

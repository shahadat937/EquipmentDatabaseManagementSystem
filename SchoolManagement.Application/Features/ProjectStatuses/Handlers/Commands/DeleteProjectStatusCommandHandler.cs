using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ProjectStatuses.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProjectStatuses.Handlers.Commands
{
    public class DeleteProjectStatusCommandHandler : IRequestHandler<DeleteProjectStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProjectStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProjectStatusCommand request, CancellationToken cancellationToken)
        {
            var ProjectStatus = await _unitOfWork.Repository<ProjectStatus>().Get(request.ProjectStatusId);

            if (ProjectStatus == null)
                throw new NotFoundException(nameof(ProjectStatus), request.ProjectStatusId);

            await _unitOfWork.Repository<ProjectStatus>().Delete(ProjectStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Project.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Project.Handlers.Commands
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var Project = await _unitOfWork.Repository<SchoolManagement.Domain.Project>().Get(request.ProjectId);

            if (Project == null)
                throw new NotFoundException(nameof(Project), request.ProjectId);

            await _unitOfWork.Repository<SchoolManagement.Domain.Project>().Delete(Project);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

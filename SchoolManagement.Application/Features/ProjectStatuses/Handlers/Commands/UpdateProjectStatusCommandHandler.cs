using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProjectStatus.Validators;
using SchoolManagement.Application.Features.ProjectStatuses.Requests.Commands;

namespace SchoolManagement.Application.Features.ProjectStatuses.Handlers.Commands
{
    public class UpdateProjectStatusCommandHandler : IRequestHandler<UpdateProjectStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProjectStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProjectStatusDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ProjectStatusDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ProjectStatus = await _unitOfWork.Repository<ProjectStatus>().Get(request.ProjectStatusDto.ProjectStatusId);

            if (ProjectStatus is null)
                throw new NotFoundException(nameof(ProjectStatus), request.ProjectStatusDto.ProjectStatusId);

            _mapper.Map(request.ProjectStatusDto, ProjectStatus);

            await _unitOfWork.Repository<ProjectStatus>().Update(ProjectStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Project.Validators;
using SchoolManagement.Application.Features.Project.Requests.Commands;

namespace SchoolManagement.Application.Features.Project.Handlers.Commands
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ProjectBaseSchoolName> _procuremenetBaseSchoolNameRepository;
       

        public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISchoolManagementRepository<ProjectBaseSchoolName> procuremenetBaseSchoolNameRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _procuremenetBaseSchoolNameRepository = procuremenetBaseSchoolNameRepository;

        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProjectDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ProjectDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Project = await _unitOfWork.Repository<SchoolManagement.Domain.Project>().Get(request.ProjectDto.ProjectId);

            if (Project is null)
                throw new NotFoundException(nameof(Project), request.ProjectDto.ProjectId);

            _mapper.Map(request.ProjectDto, Project);
          
                await _unitOfWork.Repository<SchoolManagement.Domain.Project>().Update(Project);
                await _unitOfWork.Save();

            var ProjectBaseSchool = _procuremenetBaseSchoolNameRepository.FilterWithInclude(x => x.ProjectId == request.ProjectDto.ProjectId);

           
            await _procuremenetBaseSchoolNameRepository.RemoveRangeAsync(ProjectBaseSchool);
          

            if (request.ProjectDto.BaseSchoolNameId != null && request.ProjectDto.BaseSchoolNameId.Any())
            {
                var baseSchoolNames = request.ProjectDto.BaseSchoolNameId
                    .Select(baseSchoolId => new ProjectBaseSchoolName
                    {
                        ProjectId = request.ProjectDto.ProjectId,
                        BaseSchoolNameId = baseSchoolId
                    }).ToList();

                await _unitOfWork.Repository<ProjectBaseSchoolName>().AddRangeAsync(baseSchoolNames);
                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}

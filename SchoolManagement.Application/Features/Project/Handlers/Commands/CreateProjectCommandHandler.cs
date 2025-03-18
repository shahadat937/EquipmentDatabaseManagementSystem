using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Project.Validators;
using SchoolManagement.Application.Features.Project.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Project.Handlers.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProjectDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProjectDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            try
            {
                var Project = _mapper.Map<SchoolManagement.Domain.Project>(request.ProjectDto);
                Project = await _unitOfWork.Repository<SchoolManagement.Domain.Project>().Add(Project);
                await _unitOfWork.Save();

                if (request.ProjectDto.BaseSchoolNameId != null && request.ProjectDto.BaseSchoolNameId.Any())
                {
                    var baseSchoolNames = request.ProjectDto.BaseSchoolNameId
                        .Select(baseSchoolId => new ProjectBaseSchoolName
                        {
                            ProjectId = Project.ProjectId,
                            BaseSchoolNameId = baseSchoolId 
                        }).ToList();

                    await _unitOfWork.Repository<ProjectBaseSchoolName>().AddRangeAsync(baseSchoolNames);
                    await _unitOfWork.Save();
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Project.ProjectId;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                response.Success = false;
                response.Message = "An error occurred while creating Project.";
            }

            return response;
        }

    }
}
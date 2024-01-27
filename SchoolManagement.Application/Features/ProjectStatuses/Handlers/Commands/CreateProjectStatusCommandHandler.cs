using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProjectStatus.Validators;
using SchoolManagement.Application.Features.ProjectStatuses.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProjectStatuses.Handlers.Commands
{
    public class CreateProjectStatusCommandHandler : IRequestHandler<CreateProjectStatusCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProjectStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProjectStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProjectStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProjectStatusDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ProjectStatus = _mapper.Map<ProjectStatus>(request.ProjectStatusDto);

                ProjectStatus = await _unitOfWork.Repository<ProjectStatus>().Add(ProjectStatus);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ProjectStatus.ProjectStatusId;
            }

            return response;
        }
    }
}

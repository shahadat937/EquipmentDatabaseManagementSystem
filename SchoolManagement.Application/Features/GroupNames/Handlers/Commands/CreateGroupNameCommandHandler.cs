using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.GroupNames.Validators;
using SchoolManagement.Application.Features.GroupNames.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.GroupNames.Handlers.Commands
{
    public class CreateGroupNameCommandHandler : IRequestHandler<CreateGroupNameCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateGroupNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateGroupNameCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateGroupNameDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GroupNameDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var GroupName = _mapper.Map<GroupName>(request.GroupNameDto);

                GroupName = await _unitOfWork.Repository<GroupName>().Add(GroupName);

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
                response.Id = GroupName.GroupNameId;
            }

            return response;
        }
    }
}

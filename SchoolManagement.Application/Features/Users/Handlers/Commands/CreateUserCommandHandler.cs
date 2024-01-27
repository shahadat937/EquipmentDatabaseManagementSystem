using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Users.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Users.Handlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUsersCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
         
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateUserDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.UserDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var User = _mapper.Map<User>(request.UserDto); 

                User = await _unitOfWork.Repository<User>().Add(User);
                await _unitOfWork.Save();
                
                response.Success = true;
                response.Message = "Creation Successful"; 
                response.Id = User.UserId;
            }

            return response;
        }
    }
}

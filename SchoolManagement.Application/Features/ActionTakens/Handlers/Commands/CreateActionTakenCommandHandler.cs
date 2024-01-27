using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ActionTaken.Validators;
using SchoolManagement.Application.Features.ActionTakens.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ActionTakens.Handlers.Commands
{
    public class CreateActionTakenCommandHandler : IRequestHandler<CreateActionTakenCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateActionTakenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateActionTakenCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateActionTakenDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ActionTakenDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ActionTaken = _mapper.Map<ActionTaken>(request.ActionTakenDto);

                ActionTaken = await _unitOfWork.Repository<ActionTaken>().Add(ActionTaken);

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
                response.Id = ActionTaken.ActionTakenId;
            }

            return response;
        }
    }
}

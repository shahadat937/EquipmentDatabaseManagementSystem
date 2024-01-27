using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.OperationalStates.Validators;
using SchoolManagement.Application.Features.OperationalStates.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Doamin;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.OperationalStates.Handlers.Commands
{
    public class CreateOperationalStateCommandHandler : IRequestHandler<CreateOperationalStateCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOperationalStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateOperationalStateCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateOperationalStateDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OperationalStateDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var OperationalState = _mapper.Map<OperationalState>(request.OperationalStateDto);

                OperationalState = await _unitOfWork.Repository<OperationalState>().Add(OperationalState);

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
                response.Id = OperationalState.OperationalStateId;
            }

            return response;
        }
    }
}

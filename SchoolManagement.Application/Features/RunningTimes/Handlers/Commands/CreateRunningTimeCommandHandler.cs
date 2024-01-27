using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RunningTimes.Validators;
using SchoolManagement.Application.Features.RunningTimes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.RunningTimes.Handlers.Commands
{
    public class CreateRunningTimeCommandHandler : IRequestHandler<CreateRunningTimeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRunningTimeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateRunningTimeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateRunningTimeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.RunningTimeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var RunningTime = _mapper.Map<RunningTime>(request.RunningTimeDto);

                RunningTime = await _unitOfWork.Repository<RunningTime>().Add(RunningTime);

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
                response.Id = RunningTime.RunningTimeId;
            }

            return response;
        }
    }
}

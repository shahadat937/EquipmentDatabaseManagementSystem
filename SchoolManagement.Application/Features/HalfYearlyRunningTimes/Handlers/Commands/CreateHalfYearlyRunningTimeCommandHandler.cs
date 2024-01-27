using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.HalfYearlyRunningTime.Validators;
using SchoolManagement.Application.Features.HalfYearlyRunningTimes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.HalfYearlyRunningTimes.Handlers.Commands
{
    public class CreateHalfYearlyRunningTimeCommandHandler : IRequestHandler<CreateHalfYearlyRunningTimeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHalfYearlyRunningTimeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateHalfYearlyRunningTimeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateHalfYearlyRunningTimeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.HalfYearlyRunningTimeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var HalfYearlyRunningTime = _mapper.Map<HalfYearlyRunningTime>(request.HalfYearlyRunningTimeDto);

                HalfYearlyRunningTime = await _unitOfWork.Repository<HalfYearlyRunningTime>().Add(HalfYearlyRunningTime);

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
                response.Id = HalfYearlyRunningTime.HalfYearlyRunningTimeId;
            }

            return response;
        }
    }
}

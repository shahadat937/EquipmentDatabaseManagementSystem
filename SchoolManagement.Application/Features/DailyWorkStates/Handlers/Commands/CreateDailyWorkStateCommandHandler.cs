using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DailyWorkState.Validators;
using SchoolManagement.Application.Features.DailyWorkStates.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyWorkStates.Handlers.Commands
{
    public class CreateDailyWorkStateCommandHandler : IRequestHandler<CreateDailyWorkStateCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDailyWorkStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDailyWorkStateCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDailyWorkStateDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DailyWorkStateDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                /////// File Upload //////////

                string uniqueFileName = null;

                if (request.DailyWorkStateDto.Document != null)
                {

                    var fileName = Path.GetFileName(request.DailyWorkStateDto.Document.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\daily-work-states", uniqueFileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.DailyWorkStateDto.Document.CopyToAsync(fileSteam);
                    }
                }
                var DailyWorkState = _mapper.Map<DailyWorkState>(request.DailyWorkStateDto);
                DailyWorkState.FileUpload = request.DailyWorkStateDto.FileUpload ?? "files/daily-work-states/" + uniqueFileName;
                DailyWorkState = await _unitOfWork.Repository<DailyWorkState>().Add(DailyWorkState);
                DailyWorkState.Date = DailyWorkState.Date.Value.AddDays(1.0);
                DailyWorkState.DeadLine = DailyWorkState.DeadLine.Value.AddDays(1.0);
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
                response.Id = DailyWorkState.DailyWorkStateId;
            }

            return response;
        }
    }
}

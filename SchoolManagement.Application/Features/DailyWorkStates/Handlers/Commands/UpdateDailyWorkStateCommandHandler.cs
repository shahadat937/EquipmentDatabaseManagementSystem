using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DailyWorkState.Validators;
using SchoolManagement.Application.Features.DailyWorkStates.Requests.Commands;
using FluentValidation;

namespace SchoolManagement.Application.Features.DailyWorkStates.Handlers.Commands
{
    public class UpdateDailyWorkStateCommandHandler : IRequestHandler<UpdateDailyWorkStateCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDailyWorkStateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDailyWorkStateCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDailyWorkStateDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.UpdateDailyWorkStateDto);

            //if (validationResult.IsValid == false)
                //throw new ValidationException(validationResult);

            var DailyWorkState = await _unitOfWork.Repository<DailyWorkState>().Get(request.UpdateDailyWorkStateDto.DailyWorkStateId);

            if (DailyWorkState is null)
                throw new NotFoundException(nameof(DailyWorkState), request.UpdateDailyWorkStateDto.DailyWorkStateId);

            /////// File Upload //////////
            string uniqueFileName = null;
            if (request.UpdateDailyWorkStateDto.Document != null)
            {

                var fileName = Path.GetFileName(request.UpdateDailyWorkStateDto.Document.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\daily-work-states", uniqueFileName);


                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.UpdateDailyWorkStateDto.Document.CopyToAsync(fileSteam);
                }
            }
            _mapper.Map(request.UpdateDailyWorkStateDto, DailyWorkState);
            DailyWorkState.FileUpload = request.UpdateDailyWorkStateDto.Document != null ? "files/daily-work-states/" + uniqueFileName : DailyWorkState.FileUpload.Replace("https://localhost:44395/Content/", String.Empty);
            await _unitOfWork.Repository<DailyWorkState>().Update(DailyWorkState);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

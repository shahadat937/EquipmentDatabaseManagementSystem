using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.YearSetup.Validators;
using SchoolManagement.Application.Features.YearSetups.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.YearSetups.Handlers.Commands
{
    public class CreateYearSetupCommandHandler : IRequestHandler<CreateYearSetupCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateYearSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateYearSetupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateYearSetupDtoValidator();
            var validationResult = await validator.ValidateAsync(request.YearSetupDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var YearSetup = _mapper.Map<YearSetup>(request.YearSetupDto);

                YearSetup = await _unitOfWork.Repository<YearSetup>().Add(YearSetup);

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
                response.Id = YearSetup.YearId;
            }

            return response;
        }
    }
}

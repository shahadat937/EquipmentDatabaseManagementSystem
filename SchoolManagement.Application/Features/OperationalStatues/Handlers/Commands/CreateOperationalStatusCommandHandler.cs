using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.OperationalStatuss.Validators;
using SchoolManagement.Application.Features.OperationalStatuss.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.OperationalStatuss.Handlers.Commands
{
    public class CreateOperationalStatusCommandHandler : IRequestHandler<CreateOperationalStatusCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOperationalStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateOperationalStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateOperationalStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OperationalStatusDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var OperationalStatus = _mapper.Map<OperationalStatus>(request.OperationalStatusDto);

                OperationalStatus = await _unitOfWork.Repository<OperationalStatus>().Add(OperationalStatus);

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
                response.Id = OperationalStatus.OperationalStatusId;
            }

            return response;
        }
    }
}

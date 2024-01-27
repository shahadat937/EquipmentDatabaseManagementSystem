using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.AcquisitionMethod.Validators;
using SchoolManagement.Application.Features.AcquisitionMethods.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.AcquisitionMethods.Handlers.Commands
{
    public class CreateAcquisitionMethodCommandHandler : IRequestHandler<CreateAcquisitionMethodCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAcquisitionMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateAcquisitionMethodCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateAcquisitionMethodDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AcquisitionMethodDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var AcquisitionMethod = _mapper.Map<AcquisitionMethod>(request.AcquisitionMethodDto);

                AcquisitionMethod = await _unitOfWork.Repository<AcquisitionMethod>().Add(AcquisitionMethod);

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
                response.Id = AcquisitionMethod.AcquisitionMethodId;
            }

            return response;
        }
    }
}

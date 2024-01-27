using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProcurementMethod.Validators;
using SchoolManagement.Application.Features.ProcurementMethods.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProcurementMethods.Handlers.Commands
{
    public class CreateProcurementMethodCommandHandler : IRequestHandler<CreateProcurementMethodCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProcurementMethodCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProcurementMethodCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProcurementMethodDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProcurementMethodDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ProcurementMethod = _mapper.Map<ProcurementMethod>(request.ProcurementMethodDto);

                ProcurementMethod = await _unitOfWork.Repository<ProcurementMethod>().Add(ProcurementMethod);

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
                response.Id = ProcurementMethod.ProcurementMethodId;
            }

            return response;
        }
    }
}

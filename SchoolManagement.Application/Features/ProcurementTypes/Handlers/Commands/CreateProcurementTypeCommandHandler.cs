using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProcurementType.Validators;
using SchoolManagement.Application.Features.ProcurementTypes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProcurementTypes.Handlers.Commands
{
    public class CreateProcurementTypeCommandHandler : IRequestHandler<CreateProcurementTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProcurementTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProcurementTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProcurementTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProcurementTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ProcurementType = _mapper.Map<ProcurementType>(request.ProcurementTypeDto);

                ProcurementType = await _unitOfWork.Repository<ProcurementType>().Add(ProcurementType);

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
                response.Id = ProcurementType.ProcurementTypeId;
            }

            return response;
        }
    }
}

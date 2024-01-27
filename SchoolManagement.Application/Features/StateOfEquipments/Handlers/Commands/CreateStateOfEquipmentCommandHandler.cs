using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.StateOfEquipments.Validators;
using SchoolManagement.Application.Features.StateOfEquipments.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.StateOfEquipments.Handlers.Commands
{
    public class CreateStateOfEquipmentCommandHandler : IRequestHandler<CreateStateOfEquipmentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStateOfEquipmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateStateOfEquipmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateStateOfEquipmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.StateOfEquipmentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var StateOfEquipment = _mapper.Map<StateOfEquipment>(request.StateOfEquipmentDto);

                StateOfEquipment = await _unitOfWork.Repository<StateOfEquipment>().Add(StateOfEquipment);

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
                response.Id = StateOfEquipment.StateOfEquipmentId;
            }

            return response;
        }
    }
}

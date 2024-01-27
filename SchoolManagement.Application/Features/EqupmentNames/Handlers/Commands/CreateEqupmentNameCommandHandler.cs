using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EqupmentNames.Validators;
using SchoolManagement.Application.Features.EqupmentNames.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EqupmentNames.Handlers.Commands
{
    public class CreateEqupmentNameCommandHandler : IRequestHandler<CreateEqupmentNameCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEqupmentNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateEqupmentNameCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEqupmentNameDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EqupmentNameDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var EqupmentName = _mapper.Map<EqupmentName>(request.EqupmentNameDto);

                EqupmentName = await _unitOfWork.Repository<EqupmentName>().Add(EqupmentName);

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
                response.Id = EqupmentName.EqupmentNameId;
            }

            return response;
        }
    }
}

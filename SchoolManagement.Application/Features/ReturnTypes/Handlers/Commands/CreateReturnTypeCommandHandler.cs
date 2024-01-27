using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReturnTypes.Validators;
using SchoolManagement.Application.Features.ReturnTypes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReturnTypes.Handlers.Commands
{
    public class CreateReturnTypeCommandHandler : IRequestHandler<CreateReturnTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReturnTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateReturnTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateReturnTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ReturnTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ReturnType = _mapper.Map<ReturnType>(request.ReturnTypeDto);

                ReturnType = await _unitOfWork.Repository<ReturnType>().Add(ReturnType);

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
                response.Id = ReturnType.ReturnTypeId;
            }

            return response;
        }
    }
}

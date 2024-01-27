using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CodeValues.Validators;
using SchoolManagement.Application.Features.CodeValues.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CodeValues.Handlers.Commands
{
    public class CreateCodeValueCommandHandler : IRequestHandler<CreateCodeValueCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
         
        public CreateCodeValueCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateCodeValueCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCodeValueDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.CodeValueDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var CodeValue = _mapper.Map<CodeValue>(request.CodeValueDto); 

                CodeValue = await _unitOfWork.Repository<CodeValue>().Add(CodeValue);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful"; 
                response.Id = CodeValue.CodeValueId;
            }

            return response;
        }
    }
}

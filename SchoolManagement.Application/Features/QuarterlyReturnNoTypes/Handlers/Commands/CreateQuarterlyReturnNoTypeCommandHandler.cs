using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.QuarterlyReturnNoTypes.Validators;
using SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.QuarterlyReturnNoTypes.Handlers.Commands
{
    public class CreateQuarterlyReturnNoTypeCommandHandler : IRequestHandler<CreateQuarterlyReturnNoTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateQuarterlyReturnNoTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateQuarterlyReturnNoTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateQuarterlyReturnNoTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.QuarterlyReturnNoTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var QuarterlyReturnNoType = _mapper.Map<QuarterlyReturnNoType>(request.QuarterlyReturnNoTypeDto);

                QuarterlyReturnNoType = await _unitOfWork.Repository<QuarterlyReturnNoType>().Add(QuarterlyReturnNoType);

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
                response.Id = QuarterlyReturnNoType.QuarterlyReturnNoTypeId;
            }

            return response;
        }
    }
}

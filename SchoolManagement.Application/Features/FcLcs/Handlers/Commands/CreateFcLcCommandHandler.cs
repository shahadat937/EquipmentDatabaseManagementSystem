using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FcLc.Validators;
using SchoolManagement.Application.Features.FcLcs.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FcLcs.Handlers.Commands
{
    public class CreateFcLcCommandHandler : IRequestHandler<CreateFcLcCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFcLcCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFcLcCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFcLcDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FcLcDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var FcLc = _mapper.Map<FcLc>(request.FcLcDto);

                FcLc = await _unitOfWork.Repository<FcLc>().Add(FcLc);

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
                response.Id = FcLc.FcLcId;
            }

            return response;
        }
    }
}

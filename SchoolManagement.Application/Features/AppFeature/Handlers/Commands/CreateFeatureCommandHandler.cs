using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Features.Validators;
using SchoolManagement.Application.Features.AppFeature.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.AppFeature.Handlers.Commands
{
    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
         
        public CreateFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFeatureDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.FeatureDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Feature = _mapper.Map<Feature>(request.FeatureDto); 

                Feature = await _unitOfWork.Repository<Feature>().Add(Feature);

                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful"; 
                response.Id = Feature.FeatureId;
            }

            return response;
        }
    }
}

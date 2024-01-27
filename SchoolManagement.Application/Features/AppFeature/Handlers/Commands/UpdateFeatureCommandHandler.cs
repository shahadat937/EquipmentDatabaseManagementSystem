using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Features.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.AppFeature.Requests.Commands;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.AppFeature.Handlers.Commands
{
    public class UpdateFeatureCommandHandler : IRequestHandler<UpdateFeatureCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;

        public UpdateFeatureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Unit> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateFeatureDtoValidator(); 
            var validationResult = await validator.ValidateAsync(request.FeatureDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult); 
             
            var Feature = await _unitOfWork.Repository<Feature>().Get(request.FeatureDto.FeatureId); 

            if (Feature is null)  
                throw new NotFoundException(nameof(Feature), request.FeatureDto.FeatureId); 

            _mapper.Map(request.FeatureDto, Feature);  

            await _unitOfWork.Repository<Feature>().Update(Feature); 
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}